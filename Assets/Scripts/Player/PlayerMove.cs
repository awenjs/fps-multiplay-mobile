using Mirror;
using MobileFPS.PlayerWeapon;
using UnityEngine;

namespace MobileFPS.PlayerControl
{
    public class PlayerMove : NetworkBehaviour
    {
        static readonly int   velocity = Animator.StringToHash( "velocity" );
        public          float AimSpeed, MoveSpeed, RunSpeed;
        public          float currentSpeed;
        public          float JumpHeight;
        public          float Gravity;
        CharacterController   _characterController;
        Transform             _characterTransform;
        Aim                   _gunAimState;
        Vector3               _moveDirection;
        WeaponController      _weaponController;

        void Start()
        {
            _gunAimState = GetComponentInChildren<Aim>();
            _weaponController = GetComponent<WeaponController>();
            _characterController = GetComponent<CharacterController>();
            _characterTransform = transform;
        }
        void Update()
        {
            if ( !hasAuthority ) return;

            Move();
            Crouch();
        }
        void Move()
        {
            float tmpSpeed = Input.GetKey( KeyCode.LeftShift ) ? RunSpeed : MoveSpeed;
            currentSpeed = _gunAimState.IsAiming ? AimSpeed : tmpSpeed;
            if ( _weaponController.IsAttack ) currentSpeed = AimSpeed;
            float horizontal = Input.GetAxis( "Horizontal" );
            float vertical = Input.GetAxis( "Vertical" );
            _moveDirection = _characterTransform.TransformDirection( horizontal, 0, vertical ).normalized;
            if ( !_characterController.isGrounded ) _moveDirection.y -= Gravity * Time.deltaTime;
            _characterController.Move( _moveDirection * Time.deltaTime * currentSpeed );
            var currentWeaponAnim = _weaponController.CurrentWeapon;
            Debug.Log( _characterController.velocity.magnitude );
            if ( _gunAimState.IsAiming ) currentWeaponAnim.DoAnimMovement( 0 );
            else currentWeaponAnim.DoAnimMovement( _characterController.velocity.magnitude );
        }

        void Crouch()
        {
            _characterController.height = Input.GetKey( KeyCode.LeftControl ) ? 1 : 2;
        }
    }
}
