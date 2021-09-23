using System;
using UnityEngine;
using 脚本.玩家.枪械;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    [SerializeField] int _currentAmmo;
    [SerializeField] int _magAmmo;
    [SerializeField] int _allAmmo;

    [SerializeField] float _fireRate;
    [SerializeField] int   _damage;
    [SerializeField] float _weaponRange;

    readonly int _attackAnimHash = Animator.StringToHash( "attack" );
    readonly int _reloadAnimHash = Animator.StringToHash( "reload" );

    Animator _anim;

    public int   CurrentAmmo => _currentAmmo;
    public int   MagAmmo     => _magAmmo;
    public int   AllAmmo     => _allAmmo;
    public float FireRate    => _fireRate;
    public float WeaponRange => _weaponRange;
    public void GiveAmmo(int ammo) => _allAmmo += ammo;
    void Start() => _anim = GetComponent<Animator>();

    public int GetDamage() => Random.Range( _damage - 5, _damage + 5 );

    public void DoAnimAttack() => _anim.Play( _attackAnimHash, 0, 0 );

    public void DoAnimReload() => _anim.Play( _reloadAnimHash, 0, 0 );

    public virtual void UseAmmo() => _currentAmmo--;

    public bool IsAllowedToAttack( float lastFireTime ) => Time.time - lastFireTime > 1f / _fireRate;

    public void RefillMag()
    {
        int tmpAmmo = _magAmmo - _currentAmmo;
        if ( _allAmmo < tmpAmmo )
        {
            _currentAmmo += _allAmmo;
            _allAmmo = 0;
        }
        else
        {
            _allAmmo -= tmpAmmo;
            _currentAmmo = _magAmmo;
        }
    }
}