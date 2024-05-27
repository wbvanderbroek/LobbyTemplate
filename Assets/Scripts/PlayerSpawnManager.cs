using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    private int amountOfPlayersSpawned;
    void Start()
    {
        GetComponent<NetworkManager>().ConnectionApprovalCallback = ConnectionApprovalCallback;
    }

    void ConnectionApprovalCallback(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        /* you can use this method in your project to customize one of more aspects of the player
         * (I.E: its start position, its character) and to perform additional validation checks. */
        response.Approved = true;
        response.CreatePlayerObject = true;
        response.Position = GetPlayerSpawnPosition();
        response.Rotation = GetPlayerSpawnRotation();
    }

    Vector3 GetPlayerSpawnPosition()
    {
        /*
         * this is just an example, and you change this implementation to make players spawn on specific spawn points
         * depending on other factors (I.E: player's team)
         */
        amountOfPlayersSpawned++;
        return new Vector3(55 - amountOfPlayersSpawned * 4, 6, 728);
    }
    Quaternion GetPlayerSpawnRotation()
    {
        return Quaternion.Euler(0, 50, 0);
    }
}
