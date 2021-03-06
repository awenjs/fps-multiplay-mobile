using UnityEngine;

public class Weapon : MonoBehaviour
{
    static readonly  int     velocity = Animator.StringToHash( "velocity" );
    [SerializeField] int     _currentAmmo;
    [SerializeField] int     _magAmmo;
    [SerializeField] int     _allAmmo;
    [SerializeField] float   _weaponRecoilX;
    [SerializeField] float   _weaponRecoilY;
    [SerializeField] float   _fireRate;
    [SerializeField] int     _damage;
    [SerializeField] float   _weaponRange;
    [SerializeField] Vector3 _aimPosition;

    [HideInInspector] public Animator _anim;
    public                   float    WeaponRecoilX => Random.Range( _weaponRecoilX - 3, _weaponRecoilX + 3 );
    public                   float    WeaponRecoilY => Random.Range( _weaponRecoilY - 2, _weaponRecoilY + 2 );
    public                   int      CurrentAmmo   => _currentAmmo;
    public                   int      MagAmmo       => _magAmmo;
    public                   int      AllAmmo       => _allAmmo;
    public                   float    FireRate      => _fireRate;
    public                   float    WeaponRange   => _weaponRange;
    public                   Vector3  AimPosition   => _aimPosition;
    void Start() => _anim = GetComponent<Animator>();
    public void GiveAmmo( int ammo ) => _allAmmo += ammo;

    public int GetDamage() => Random.Range( _damage - 5, _damage + 5 );

    public void DoAnimAttack() => _anim.Play( "Attack", 0, 0 );
    public void DoAnimReload() => _anim.Play( "Reload", 0, 0 );
    public void DoAnimMovement( float speed ) => _anim.SetFloat( velocity, speed, 0.1f, Time.deltaTime );


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
