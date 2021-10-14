using Mirror;
using MobileFPS.PlayerHealth;
using UnityEngine;

namespace MobileFPS.PlayerWeapon
{
    public class WeaponController : NetworkBehaviour
    {
        [SerializeField]         Transform _hand;
        [SerializeField]         Bag       _guns;
        [HideInInspector] public Weapon    CurrentWeapon;
        
        [SerializeField] float   aimSpeed;
        Transform                _cam;
        Weapon                   _defaultWeapon;
        float                    _lastFireTime;
        CameraController         _cameraController;
        public bool  IsAttack;
        void Awake()
        {
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
            _cam = Camera.main.transform;
            _cameraController = GetComponent<CameraController>();
            _guns = GetComponent<Bag>();
            _defaultWeapon = _guns.MainWeapon;
            SwapWeapon( _defaultWeapon );
        }

        void SwapWeapon( Weapon newWeapon )
        {
            if ( newWeapon == null ) return;

            foreach ( Transform t in _hand )
                if ( t.gameObject.name == newWeapon.gameObject.name ) t.gameObject.SetActive( true );
                else t.gameObject.SetActive( false );
            CurrentWeapon = newWeapon;
        }

        void DoSwapWeapon()
        {
            if ( Input.GetKeyDown( KeyCode.Alpha1 ) ) SwapWeapon( _guns.MainWeapon );
        }

        void DoAttack()
        {
            if ( !Input.GetMouseButton( 0 ) ) return;
            if ( CurrentWeapon.CurrentAmmo == 0 || !CurrentWeapon.IsAllowedToAttack( _lastFireTime ) ) return;
            
            _cameraController.Recoil( CurrentWeapon.WeaponRecoilY );
            _lastFireTime = Time.time;
            CurrentWeapon.UseAmmo();
            CmdRequestAttack( _cam.position, _cam.forward, CurrentWeapon.GetDamage(), CurrentWeapon.WeaponRange );
            CurrentWeapon.DoAnimAttack();
        }


        void DoReload()
        {
            if ( !Input.GetKeyDown( KeyCode.R ) ) return;

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
