using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class PlayerMove : NetworkBehaviour
{
    public Joystick joystick;
    private CharacterController _characterController;
    private Transform _characterTransform;
    private Vector3 _moveDirection;
    public float MoveSpeed , QuietMoveSpeed; 
    float currentSpeed;
    public float JumpHeight;
    public float Gravity;

    public bool PC;
    void Start()
    {
        joystick = GameObject.Find("Fixed Joystick").GetComponent<Joystick>();
        _characterController = GetComponent<CharacterController>();
        _characterTransform = transform;
    }
    void Update()
    {
        if(!hasAuthority)return;
        Move();
        Crouch();
        if(PC) return;
        MobileMove();
    }
    void Move()
    {
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? QuietMoveSpeed : MoveSpeed;
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        _moveDirection = _characterTransform.TransformDirection(horizontal, 0, vertical);
        if (!_characterController.isGrounded)
        {
            _moveDirection.y -= Gravity * Time.deltaTime;
        }
        _characterController.Move(_moveDirection * Time.deltaTime * currentSpeed);
    }

    void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            _characterController.height = 1;
        }
        else
        {
            _characterController.height = 2;
        }
        
    }

    void MobileMove()
    {
        //currentSpeed = Input.GetKey(KeyCode.LeftShift) ? QuietMoveSpeed : MoveSpeed;
        var horizontal = joystick.Horizontal;
        var vertical = joystick.Vertical;
        _moveDirection = _characterTransform.TransformDirection(horizontal, 0, vertical);
        if (!_characterController.isGrounded)
        {
            _moveDirection.y -= Gravity * Time.deltaTime;
        }
        _characterController.Move(_moveDirection * Time.deltaTime * currentSpeed);
    }
}
