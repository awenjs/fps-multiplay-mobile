using Mirror;
using MobileFPS.Player;
using MobileFPS.PlayerUI;
using UnityEngine;

namespace MobileFPS.PlayerHealth
{
    public class Health : NetworkBehaviour
    {
        [SerializeField] PlayerUIManager _playerUIManager;
        [SerializeField] PlayerManager   _playerManager;
        public           int             MaxHealth;
        [field: SyncVar]
        public int CurrentHealth { get; private set; }

        void Awake()
        {
            CurrentHealth = MaxHealth;
        }
    

        [Server]
        public void Damage( int damage )
        {
            int result = CurrentHealth - damage;

            if ( result <= 0 )
            {
                CurrentHealth = 0;
                DoDie();
                return;
            }
            //else

            CurrentHealth = result;
        }
        void DoDie()
        {
            Cursor.lockState = CursorLockMode.None;
            _playerUIManager.PlayerDead();
            _playerManager.MainCameraState( true );
            Debug.Log( "dead" );
            NetworkServer.Destroy( gameObject );
        }
    }
}
