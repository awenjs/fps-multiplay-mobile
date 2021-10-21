using Mirror;
using MobileFPS.PlayerHealth;
using MobileFPS.PlayerWeapon;
using UnityEngine;

namespace MobileFPS.PlayerUI
{
    public class PlayerUIManager : NetworkBehaviour
    {
        GameManager      _gameManager;
        Health           _playerHealth;
        WeaponController _weaponController;
        void Awake()
        {
            _gameManager = GameObject.Find( "GameManager" ).GetComponent<GameManager>();
            
        }
        void Update()
        {
            if ( !hasAuthority ) return;

            UpdatePlayerAmmo();
            UpdatePlayerHealth();
        }

        public override void OnStartAuthority()
        {
            _weaponController = GetComponent<WeaponController>();
            _playerHealth = GetComponent<Health>();
            OpenUI();
            PlayerAliveUI();
        }

        public void PlayerDead()
        {
            _gameManager._deadUI.SetActive( true );
            _gameManager._aliveUI.SetActive( false );
        }

        void PlayerAliveUI()
        { 
            _gameManager._deadUI.SetActive( false );
            _gameManager._aliveUI.SetActive( true );
        }

        void UpdatePlayerAmmo()
        {
            var currentWeapon = _weaponController.CurrentWeapon;
            _gameManager.Ammo.text = currentWeapon.CurrentAmmo + "/" + currentWeapon.AllAmmo;
        }

        void UpdatePlayerHealth()
        {
            var currentHealth = _playerHealth;
            _gameManager.Health.text = currentHealth.CurrentHealth.ToString();
        }

        void OpenUI()
        {
            enabled = true;
            _gameManager._canvas.gameObject.SetActive( true );
        }
    }
}
