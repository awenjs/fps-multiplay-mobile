using Mirror;
using UnityEngine;

public class CameraController : NetworkBehaviour
{
    public Transform  player;
    public GameObject cam;
    public float      mouseSensitivity;
    public float      mobileX, mobileY; //鼠标灵敏度
    public float      xRotation;
    GameManager       _gameManager;
    FixedTouchField   _touchField;
    float             mouseX, mouseY; //获取鼠标移动的值
    void Awake()
    {
        _gameManager = GameObject.Find( "GameManager" ).GetComponent<GameManager>();
        _touchField = GameObject.Find( "TouchPanel" ).GetComponent<FixedTouchField>();
    }

    void Update()
    {
        if ( !hasAuthority ) return;

        CameraMobile();
        if ( !_gameManager.PC ) return;

        Camera();
    }
    public override void OnStartAuthority()
    {
        cam.SetActive( true );
        if ( !_gameManager.PC ) return;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void CameraMobile()
    {
        mobileX = _touchField.TouchDist.x;
        mobileY = _touchField.TouchDist.y;
        mouseX = mobileX * mouseSensitivity * Time.deltaTime;
        mouseY = mobileY * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp( xRotation, -90f, 90f );
        player.Rotate( Vector3.up * mouseX );
        cam.transform.localRotation = Quaternion.Euler( xRotation, 0, 0 );
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
