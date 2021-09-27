using System;
using Mirror;
using UnityEngine;

public class CameraController : NetworkBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform cam;
    public           float     mouseSensitivity;
    public           float     mobileSensitivity;
    float                      xRotation;
    void Update()
    {
        if ( !hasAuthority ) return;
        //本地验证
        if(Application.platform == RuntimePlatform.Android)
            mobileInput();
        else
            Camera();
    }
    public override void OnStartAuthority()
    {
        cam.gameObject.SetActive( true );
        Cursor.lockState = CursorLockMode.Locked;
    }

    void CameraMobile( Vector2 vec )
    {
        float mouseX = vec.x * mobileSensitivity * Time.deltaTime;
        float mouseY = vec.y * mobileSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp( xRotation, -90f, 90f );
        player.Rotate( Vector3.up * mouseX );
        cam.localRotation = Quaternion.Euler( xRotation, 0, 0 );
    }

    void mobileInput()
    {
        if ( Input.touchCount == 0 ) return;
        //else

        foreach ( var touch in Input.touches )
        {

            if ( !IsInLookArea( touch ) ) continue;
            //else

            CameraMobile( touch.deltaPosition );
            return;
        }

        bool IsInLookArea( Touch touch ) => touch.position.x > Screen.width / 2f;
    }

    void Camera()
    {
        float mouseX = Input.GetAxisRaw( "Mouse X" ) * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw( "Mouse Y" ) * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp( xRotation, -90f, 90f );
        player.Rotate( Vector3.up * mouseX );
        cam.transform.localRotation = Quaternion.Euler( xRotation, 0, 0 );
    }
}
