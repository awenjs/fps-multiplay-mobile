using System;
using UnityEngine;
using Mirror;
public class GameNetManagerHud : MonoBehaviour
{
    [SerializeField] GameNetManager _netManager;
    [SerializeField] GameObject     _gamePanel;
    void Update()
    {
        if ( !NetworkClient.isConnected && !NetworkServer.active )
        {
            
        }
        else
        {
            _gamePanel.SetActive( true );
        }
    }
    public void Menu()
    {
        
    }
    public void Host() => _netManager.StartHost();
    public void Client() => _netManager.StartClient();
    public void Server() => _netManager.StartServer();
}
