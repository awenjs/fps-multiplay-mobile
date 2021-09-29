using Mirror;
using UnityEngine;

namespace MobileFPS.PlayerControl
{
    public class PlayerMove : NetworkBehaviour
    {
        public Joystick     joystick;
        public float        MoveSpeed, QuietMoveSpeed;
        public float        currentSpeed;
        public float        JumpHeight;
        public float        Gravity;
        CharacterController _characterController;
        Transform           _characterTransform;
        Vector3             _moveDirection;

        void Start()
        {
            joystick = GameObject.Find( "Floating Joystick" ).GetComponent<FloatingJoystick>();
            _characterController = GetComponent<CharacterController>();
            _characterTransform = transform;
        }
        void Update()
        {
            if ( !hasAuthority ) return;

            if ( Application.platform == RuntimePlatform.Android )
            {
                MobileMove();
            }
            else
            {
                Move();
                Crouch();
            }
        }
        void Move()
        {
            currentSpeed = Input.GetKey( KeyCode.LeftShift ) ? QuietMoveSpeed : MoveSpeed;
            float horizontal = Input.GetAxis( "Horizontal" );
            float vertical = Input.GetAxis( "Vertical" );
            _moveDirection = _characterTransform.TransformDirection( horizontal, 0, vertical );
            if ( !_characterController.isGrounded ) _moveDirection.y -= Gravity * Time.deltaTime;
            _characterController.Move( _moveDirection * Time.deltaTime * currentSpeed );
        }

        void Crouch()
        {
            if ( Input.GetKey( KeyCode.LeftControl ) ) _characterController.height = 1;
            else _characterController.height = 2;

        }

        void MobileMove()
        {
            float horizontal = joystick.Horizontal;
            float vertical = joystick.Vertical;
            _moveDirection = _characterTransform.TransformDirection( horizontal, 0, vertical );
            if ( !_characterController.isGrounded ) _moveDirection.y -= Gravity * Time.deltaTime;
            _characterController.Move( _moveDirection * Time.deltaTime * currentSpeed );
        }
    }
}
