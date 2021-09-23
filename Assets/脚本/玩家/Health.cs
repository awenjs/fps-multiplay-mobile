using System;
using Mirror;
using UnityEngine;

public class Health : NetworkBehaviour
{
    [SerializeField] PlayerManager _playerManager;
    [SerializeField] int _maxHealth;

    [field: SyncVar( hook = nameof( OnHealthChangedEvent ) )]
    public int CurrentHealth { get; private set; }

    void Awake() => CurrentHealth = _maxHealth;

    public event Action<int, int> OnHealthChanged;
    public event Action           OnDeath;

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

    void OnHealthChangedEvent( int oldHealth, int newHealth ) => OnHealthChanged?.Invoke( oldHealth, newHealth );

    void DoDie()
    {
        Destroy(gameObject);
        _playerManager.MainCameraState(true);
        OnDeath?.Invoke();
        Debug.Log( "dead" );
        
    }
}