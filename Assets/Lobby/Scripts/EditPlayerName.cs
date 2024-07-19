using TMPro;
using UnityEngine;

public class EditPlayerName : MonoBehaviour
{
    public static EditPlayerName Instance { get; private set; }
    [SerializeField] private TMP_InputField playerNameText;
    private string playerName = "Player";

    private void Awake()
    {
        Instance = this;
        playerNameText.onEndEdit.AddListener(delegate { OnNameChanged(); });
        playerNameText.text = playerName;
    }
    private void OnNameChanged()
    {
        LobbyManager.Instance.UpdatePlayerName(GetPlayerName());
    }
    public string GetPlayerName()
    {
        return playerNameText.text.Length > 0 ? playerNameText.text : "Player";
    }
}