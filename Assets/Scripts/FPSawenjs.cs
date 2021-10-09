using UnityEngine;

namespace awenjsFps
{
    public class CameraControll : MonoBehaviour
    {
        public Transform  player;
        public GameObject cam;
        public float      mouseSensitivity; //鼠标灵敏度
        float      xRotation;
        float             mouseX, mouseY; //获取鼠标移动的值

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            Camera();
        }

        void Camera()
        {
            mouseX = Input.GetAxisRaw( "Mouse X" ) * mouseSensitivity * Time.deltaTime;
            mouseY = Input.GetAxisRaw( "Mouse Y" ) * mouseSensitivity * Time.deltaTime;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp( xRotation, -90f, 90f );
            player.Rotate( Vector3.up * mouseX );
            cam.transform.localRotation = Quaternion.Euler( xRotation, 0, 0 );
        }
    }

    public class MoveControll : MonoBehaviour
    {
        [Header("PlayerMoveSpeed")]
        public float        
            currentSpeed,
            MoveSpeed, 
            RunSpeed;
        
        public float        JumpHeight;
        public float        Gravity;
        CharacterController _characterController;
        Transform           _characterTransform;
        Vector3             _moveDirection;

        void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _characterTransform = transform;
        }
        void Update()
        {
            Move();
        }
        void Move()
        {
            currentSpeed = Input.GetKey( KeyCode.LeftShift ) ? RunSpeed : MoveSpeed;
            float horizontal = Input.GetAxis( "Horizontal" );
            float vertical = Input.GetAxis( "Vertical" );
            _moveDirection = _characterTransform.TransformDirection( horizontal, 0, vertical );
            if ( !_characterController.isGrounded ) _moveDirection.y -= Gravity * Time.deltaTime;
            _characterController.Move( _moveDirection * Time.deltaTime * currentSpeed );
        }
    }
}
