using Mirror;
using UnityEngine;

namespace 脚本.玩家.枪械
{
    public class WeaponController : NetworkBehaviour
    {
        [SerializeField] Weapon    _defaultWeapon;
        [SerializeField] Transform hand;
        [SerializeField] Bag       _guns;
        [SerializeField] LayerMask _mask;
        FixedButton                _attackButton, _mainWeapon, _melee, _reload;

        Camera        _cam;
        GameManager   _gameManager;
        float         _lastFireTime;
        public Weapon CurrentWeapon { get; private set; }

        void Awake()
        {
            _gameManager = GameObject.Find( "GameManager" ).GetComponent<GameManager>();
            _attackButton = GameObject.Find( "Shoot" ).GetComponent<FixedButton>();
            _mainWeapon = GameObject.Find( "MainWeapon" ).GetComponent<FixedButton>();
            _melee = GameObject.Find( "Melee" ).GetComponent<FixedButton>();
            _reload = GameObject.Find( "Reload" ).GetComponent<FixedButton>();
        }
        void Update()
        {
            if ( !hasAuthority ) return;

            DoSwapWeaponMobile();
            DoAttackMobile();
            DoReloadMobile();
            if ( !_gameManager.PC ) return;

            DoSwapWeapon();
            DoAttack();
            DoReload();
        }

        public override void OnStartAuthority()
        {
            _cam = Camera.main;
            _guns = GetComponent<Bag>();
            _defaultWeapon = _guns.Knife;
            SwapWeapon( _defaultWeapon );
        }

        void DoSwapWeapon()
        {
            if ( Input.GetKeyDown( KeyCode.Alpha1 ) ) SwapWeapon( _guns.MainWeapon );

            if ( Input.GetKeyDown( KeyCode.Alpha2 ) )
            {
                //SwapWeapon(_guns.Second);
            }

            if ( Input.GetKeyDown( KeyCode.Alpha3 ) )
                SwapWeapon( _guns.Knife );
        }

        void DoSwapWeaponMobile()
        {
            if ( _mainWeapon.Pressed ) SwapWeapon( _guns.MainWeapon );
            if ( _melee.Pressed )
                SwapWeapon( _guns.Knife );
        }

        void SwapWeapon( Weapon newWeapon )
        {
            if ( newWeapon == null ) return;

            foreach ( Transform t in hand )
                if ( t.gameObject.name == newWeapon.gameObject.name ) t.gameObject.SetActive( true );
                else t.gameObject.SetActive( false );
            CurrentWeapon = newWeapon;
        }

        void DoAttack()
        {
            if ( !Input.GetMouseButton( 0 ) ) return;
            if ( CurrentWeapon.CurrentAmmo == 0 || !CurrentWeapon.IsAllowedToAttack( _lastFireTime ) ) return;

            _lastFireTime = Time.time;
            CurrentWeapon.UseAmmo();
            CmdRequestAttack( _cam.transform.position, _cam.transform.forward, CurrentWeapon.GetDamage(), CurrentWeapon.WeaponRange );
            CurrentWeapon.DoAnimAttack();
        }

        void DoAttackMobile()
        {
            if ( !_attackButton.Pressed ) return;
            if ( CurrentWeapon.CurrentAmmo == 0 || !CurrentWeapon.IsAllowedToAttack( _lastFireTime ) ) return;

            _lastFireTime = Time.time;
            CurrentWeapon.UseAmmo();
            CmdRequestAttack( _cam.transform.position, _cam.transform.forward, CurrentWeapon.GetDamage(), CurrentWeapon.WeaponRange );
            CurrentWeapon.DoAnimAttack();
        }

        void DoReload()
        {
            if ( !Input.GetKeyDown( KeyCode.R ) ) return;

            if ( CurrentWeapon.CurrentAmmo == CurrentWeapon.MagAmmo || CurrentWeapon.AllAmmo == 0 ) return;

            CurrentWeapon.DoAnimReload();
        }

        void DoReloadMobile()
        {
            if ( !_reload.Pressed ) return;

            if ( CurrentWeapon.CurrentAmmo == CurrentWeapon.MagAmmo || CurrentWeapon.AllAmmo == 0 ) return;

            CurrentWeapon.DoAnimReload();
        }

        [Command]
        void CmdRequestAttack( Vector3 camPos, Vector3 camForward, int damage, float range )
        {
            if ( !Physics.Raycast( camPos, camForward, out var hitInfo, range, _mask.value ) ) return;

            Debug.Log( "Server: Player hit " + hitInfo.collider.name );

            if ( !hitInfo.collider.CompareTag( "Player" ) ) return;
            //else

            hitInfo.collider.GetComponent<Health>().Damage( damage );
        }
    }
}
