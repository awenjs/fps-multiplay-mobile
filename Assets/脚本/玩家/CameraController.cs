using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class CameraController : NetworkBehaviour
{
    public Transform player;
    public GameObject cam;
    private float mouseX, mouseY; //获取鼠标移动的值
    public float mouseSensitivity; //鼠标灵敏度
    public float xRotation;

    public override void OnStartAuthority()
    {
        cam.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update () {
        if (!hasAuthority) return;
            Camera();
    }

    void Camera()
    {
        mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp (xRotation, -90f, 90f);
        player.Rotate (Vector3.up * mouseX);
        cam.transform.localRotation = Quaternion.Euler (xRotation, 0, 0); 
    }
}