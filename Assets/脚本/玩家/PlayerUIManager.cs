using Mirror;
using UnityEngine;
using 脚本.玩家.枪械;

public class PlayerUIManager : NetworkBehaviour
{
    Health           _playerHealth;
    WeaponController _weaponController;
    GameManager      _gameManager;
    void Awake()
    {
        _gameManager = GameObject.Find( "GameManager" ).GetComponent<GameManager>();
    }
    
    [Client]
    void Update()
    {
        if ( !hasAuthority ) return;

        UpdatePlayerAmmo();
        UpdatePlayerHealth();
    }

    public override void OnStartAuthority()
    {
        OpenUI();
        _weaponController = GetComponent<WeaponController>();
        _playerHealth = GetComponent<Health>();
    }

    void UpdatePlayerAmmo()
    {
        var currentWeapon = _weaponController.CurrentWeapon;
        _gameManager.Ammo().text = currentWeapon.CurrentAmmo + "/" + currentWeapon.AllAmmo;
    }

    void UpdatePlayerHealth()
    {
        var currentHealth = _playerHealth;
        _gameManager.Health().text = currentHealth.CurrentHealth.ToString();
    }
    
    void OpenUI()
    {
        enabled = true;
        _gameManager._canvas.gameObject.SetActive( true );
    }
}