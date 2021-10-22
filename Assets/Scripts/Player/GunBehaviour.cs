using System.Collections;
using System.Collections.Generic;
using System.Net;
using MobileFPS.PlayerWeapon;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    WeaponController         _weaponController;
    [SerializeField] Vector3 
        _gunFireOffset,
        _gunNotFireOffset,
        _gunNotAimOffset;
    Vector3     _gunRecoilOffset;
    Vector3     _gunAimOffset;
    public bool IsAim;
    void Start()
    {
        _weaponController = GetComponentInParent<WeaponController>();
    }
    void Update()
    {
        Mod();
    }
    void Mod()
    {
        if ( Input.GetMouseButton( 0 ) && _weaponController.CurrentWeapon.CurrentAmmo != 0)
        {
            _gunRecoilOffset = Vector3.Slerp( _gunRecoilOffset, _gunFireOffset, 15f * Time.deltaTime );
        }
        else
        {
            _gunRecoilOffset = Vector3.Slerp( _gunRecoilOffset, _gunNotFireOffset, 5f * Time.deltaTime );
        }
        
        if ( Input.GetMouseButton( 1 ) )
        {
            IsAim = true;
            _gunAimOffset = Vector3.Slerp( _gunAimOffset, _weaponController.CurrentWeapon.AimPosition, 25f * Time.deltaTime );
        }
        else
        {
            IsAim = false;
            _gunAimOffset = Vector3.Slerp( _gunAimOffset, _gunNotAimOffset, 25f * Time.deltaTime );
        }
        transform.localPosition = _gunRecoilOffset + _gunAimOffset;
    }
}
