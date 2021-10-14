using System;
using Mirror;
using MobileFPS.PlayerWeapon;
using UnityEngine;

namespace MobileFPS.PlayerHealth
{
    public class CameraController : NetworkBehaviour
    {
        [SerializeField] Transform player;
        [SerializeField] Transform cam;
        float                      RecoilOffset;
        public float               Sensitivity;
        float                      xRotation;
        void Update()
        {
            if ( !hasAuthority ) return;
            //本地验证
            Camera();
        }
        public override void OnStartAuthority()
        {
            cam.gameObject.SetActive( true ); 
            Cursor.lockState = CursorLockMode.Locked;
        }
        void Camera()
        {
            RecoilOffset = Mathf.Lerp( RecoilOffset, 0f, 10f * Time.deltaTime );
            float mouseX = Input.GetAxisRaw( "Mouse X" ) * Sensitivity * Time.deltaTime;
            float mouseY = Input.GetAxisRaw( "Mouse Y" ) * Sensitivity * Time.deltaTime;
            mouseY += RecoilOffset;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp( xRotation, -90f, 90f );
            player.Rotate( Vector3.up * mouseX );
            cam.transform.localRotation = Quaternion.Euler( xRotation, 0, 0 );
        }

        public void Recoil(float recoil)
        {
            RecoilOffset += recoil * Time.deltaTime;
        }
    }
}
