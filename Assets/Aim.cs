using MobileFPS.PlayerWeapon;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] WeaponController _weaponController;
    [SerializeField] Vector3          notAimVec;
    [SerializeField] Vector3          aimVec;
    public           bool             IsAiming;

    void Start()
    {
        _weaponController = GetComponentInParent<WeaponController>();
    }
    void Update()
    {
        Aiming();
    }
    void Aiming()
    {
        if ( Input.GetMouseButton( 1 ) )
        {
            var currentWeapon = _weaponController.CurrentWeapon;
            if ( currentWeapon is Melee ) return;

            IsAiming = true;
            transform.localPosition = Vector3.Slerp( transform.localPosition, aimVec, currentWeapon.aimSpeed * Time.deltaTime );
        }
        else
        {
            var currentWeapon = _weaponController.CurrentWeapon;
            IsAiming = false;
            transform.localPosition = Vector3.Slerp( transform.localPosition,notAimVec, currentWeapon.aimSpeed * Time.deltaTime );
        }
    }
}
