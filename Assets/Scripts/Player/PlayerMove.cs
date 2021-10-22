using Mirror;
using MobileFPS.PlayerWeapon;
using UnityEngine;

namespace MobileFPS.PlayerControl
{
    public class PlayerMove : NetworkBehaviour
    {
        static readonly int   velocity = Animator.StringToHash( "velocity" );
        public          float CombatSpeed, MoveSpeed, RunSpeed;
        public          float currentSpeed;
        public          float JumpHeight;
        public          float Gravity;
        GunBehaviour             _gunBehaviour;
        CharacterController   _characterController;
        Transform             _characterTransform;
        Vector3               _moveDirection;
        WeaponController      _weaponController;
        public float          PlayerCurrentSpeed;

        void Start()
        {
            _gunBehaviour = GetComponentInChildren<GunBehaviour>();
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
            var currentWeaponAnim = _weaponController.CurrentWeapon;
            float horizontal = Input.GetAxisRaw( "Horizontal" );
            float vertical = Input.GetAxisRaw( "Vertical" );

            float tmpSpeed = Input.GetKey( KeyCode.LeftShift ) ? RunSpeed : MoveSpeed;
            if ( _gunBehaviour.IsAim) tmpSpeed = CombatSpeed;
            currentSpeed = tmpSpeed;

            _moveDirection = _characterTransform.TransformDirection( horizontal, 0, vertical ).normalized;
            if ( !_characterController.isGrounded ) _moveDirection.y -= Gravity * Time.deltaTime;
            _characterController.Move( _moveDirection * Time.deltaTime * currentSpeed );
            var tmp_Velocity = _characterController.velocity;
            tmp_Velocity.y = 0;
            PlayerCurrentSpeed = tmp_Velocity.magnitude;
            //Debug.Log( "当前移速:" +  PlayerCurrentSpeed);
            
            if ( _gunBehaviour.IsAim ) currentWeaponAnim._anim.SetFloat( "velocity",0 );
            else currentWeaponAnim.DoAnimMovement( PlayerCurrentSpeed );
        }

        void Crouch()
        {
            _characterController.height = Input.GetKey( KeyCode.LeftControl ) ? 1 : 2;
        }
    }
}
