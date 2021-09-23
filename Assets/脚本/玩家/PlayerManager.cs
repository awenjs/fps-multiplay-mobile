using System;
using Mirror;
using UnityEngine;
using 脚本.玩家.枪械;

public class PlayerManager : NetworkBehaviour
{
    const string LocalPlayerLayer = "LocalPlayer";

    GameManager _gameManager;

    WeaponController _weaponController;
    
    [SerializeField] NetworkBehaviour[] _toggleNetworkBehaviour;

    void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _weaponController = GetComponent<WeaponController>();
    }

    public override void OnStartAuthority()
    {
        ChangeLocalPlayerLayer();
        
        MainCameraState(false);
        
        foreach ( var behaviour in _toggleNetworkBehaviour ) behaviour.enabled = true;
    }

    void ChangeLocalPlayerLayer() => gameObject.layer = LayerMask.NameToLayer( LocalPlayerLayer );

    public void MainCameraState(bool check) => _gameManager.Camera().SetActive(check);

    void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("AmmoBox")) return;
        _weaponController.CurrentWeapon.GiveAmmo(60);
        Destroy(other.gameObject);
    }
}

