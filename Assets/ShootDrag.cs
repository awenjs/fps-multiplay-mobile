using UnityEngine;

public class ShootDrag : MonoBehaviour
{
    [SerializeField] CameraController _cameraController;
    [SerializeField] FixedButton      _fire;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!_fire.Pressed) return;
    }
}
