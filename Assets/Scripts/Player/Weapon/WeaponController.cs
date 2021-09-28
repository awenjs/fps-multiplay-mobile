using Mirror;
using UnityEngine;

namespace Player.Weapon
{
    public class WeaponController : NetworkBehaviour
    {
        [SerializeField] Transform _hand;
        [SerializeField] Bag       _guns;
        [SerializeField] LayerMask _mask;

        FixedButton _attackButton,
            _reloadButton,
            _mainWeaponButton,
            _meleeWeaponButton;

        Transform             _cam;
        global::Weapon        _defaultWeapon;
        GameManager           _gameManager;
        float                 _lastFireTime;
        public global::Weapon CurrentWeapon { get; private set; }

        void Awake()
        {
            _gameManager = GameObject.Find( "GameManager" ).GetComponent<GameManager>();
        }
        void Update()
        {
            if ( !hasAuthority ) return;

            DoSwapWeapon();
            DoAttack();
            DoReload();
        }
        public override void OnStartAuthority()
        {
            _attackButton = GameObject.Find( "ShootButton" ).GetComponent<FixedButton>();
            _reloadButton = GameObject.Find( "ReloadButton" ).GetComponent<FixedButton>();
            _mainWeaponButton = GameObject.Find( "MainWeaponButton" ).GetComponent<FixedButton>();
            _meleeWeaponButton = GameObject.Find( "MeleeButton" ).GetComponent<FixedButton>();
            _cam = Camera.main.transform;
            _guns = GetComponent<Bag>();
            _defaultWeapon = _guns.Knife;
            SwapWeapon( _defaultWeapon );
        }

        void SwapWeapon( global::Weapon newWeapon )
        {
            if ( newWeapon == null ) return;

            foreach ( Transform t in _hand )
                if ( t.gameObject.name == newWeapon.gameObject.name ) t.gameObject.SetActive( true );
                else t.gameObject.SetActive( false );
            CurrentWeapon = newWeapon;
        }

        void DoSwapWeapon()
        {
            if ( Input.GetKeyDown( KeyCode.Alpha1 ) || _mainWeaponButton.Pressed ) SwapWeapon( _guns.MainWeapon );
            if ( Input.GetKeyDown( KeyCode.Alpha3 ) || _meleeWeaponButton.Pressed ) SwapWeapon( _guns.Knife );
        }

        void DoAttack()
        {
            if ( !_attackButton.Pressed ) return;
            if ( CurrentWeapon.CurrentAmmo == 0 || !CurrentWeapon.IsAllowedToAttack( _lastFireTime ) ) return;

            _lastFireTime = Time.time;
            CurrentWeapon.UseAmmo();
            CmdRequestAttack( _cam.position, _cam.forward, CurrentWeapon.GetDamage(), CurrentWeapon.WeaponRange );
            CurrentWeapon.DoAnimAttack();
        }


        void DoReload()
        {
            if ( !Input.GetKeyDown( KeyCode.R ) && !_reloadButton.Pressed ) return;

            if ( CurrentWeapon.CurrentAmmo == CurrentWeapon.MagAmmo || CurrentWeapon.AllAmmo == 0 ) return;

            CurrentWeapon.DoAnimReload();
        }

        [Command]
        void CmdRequestAttack( Vector3 camPos, Vector3 camForward, int damage, float range )
        {
            if ( !Physics.Raycast( camPos, camForward, out var hitInfo, range ) ) return;

            Debug.Log( "Server: Player hit " + hitInfo.collider.name );
            if ( !hitInfo.collider.CompareTag( "Player" ) ) return;

            //else
            hitInfo.collider.GetComponent<Health>().Damage( damage );
        }
    }
}
