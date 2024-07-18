using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditPlayerName : MonoBehaviour 
{
    public static EditPlayerName Instance { get; private set; }
    public event EventHandler OnNameChanged;
    [SerializeField] private TMP_InputField playerNameText;
    private string playerName = "Player";

    private void Awake() {
        Instance = this;

        playerNameText.onEndEdit.AddListener(delegate { EditPlayerName_OnNameChanged(this, EventArgs.Empty); });

        playerNameText.text = playerName;
    }
    private void Start() {
        OnNameChanged += EditPlayerName_OnNameChanged;
    }
    private void EditPlayerName_OnNameChanged(object sender, EventArgs e) {
        LobbyManager.Instance.UpdatePlayerName(GetPlayerName());
    }
    public string GetPlayerName() {
        return playerNameText.text.Length > 0 ? playerNameText.text : "Player";

    }
}