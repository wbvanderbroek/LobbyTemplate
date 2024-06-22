using UnityEngine;

public class Authenticate : MonoBehaviour
{
    private void Start()
    {
        LobbyManager.Instance.Authenticate(EditPlayerName.Instance.GetPlayerName());
    }
}