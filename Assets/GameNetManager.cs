using UnityEngine;
using Mirror;
using UnityEngine.PlayerLoop;

public class GameNetManager : NetworkManager
{
    void Update()
    {
        //Respawn();
    }
    public void Respawn()
    {
        NetworkClient.AddPlayer();
    }
}
