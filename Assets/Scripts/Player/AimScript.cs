using System.Collections;
using System.Collections.Generic;
using System.Net;
using MobileFPS.PlayerWeapon;
using UnityEngine;

public class AimScript : MonoBehaviour
{
    WeaponController         _weaponController;
    [SerializeField] Vector3 _notAim;
    public           bool    IsAim;
    void Start()
    {
        _weaponController = GetComponentInParent<WeaponController>();
    }
    void Update()
    {
        Aim();
    }
    void Aim()
    {
        if ( Input.GetMouseButton( 1 ) )
        {
            IsAim = true;
            transform.localPosition = Vector3.Slerp( transform.localPosition, _weaponController.CurrentWeapon.AimPosition, 25f * Time.deltaTime );
        }
        else
        {
            IsAim = false;
            transform.localPosition = Vector3.Slerp( transform.localPosition, _notAim, 25f * Time.deltaTime );
        }
    }
}
