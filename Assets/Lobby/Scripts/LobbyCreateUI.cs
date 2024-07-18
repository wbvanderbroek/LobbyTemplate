using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyCreateUI : MonoBehaviour 
{
    public static LobbyCreateUI Instance { get; private set; }

    [SerializeField] private Button createButton;
    [SerializeField] private Button publicPrivateButton;
    [SerializeField] private Button gameModeButton;
    [SerializeField] private TMP_InputField lobbyNameInputField;
    [SerializeField] private TextMeshProUGUI publicPrivateText;
    [SerializeField] private TMP_InputField maxPlayersInputField;
    [SerializeField] private TextMeshProUGUI gameModeText;

    private bool isPrivate;
    private LobbyManager.GameMode gameMode;

    private void Awake() {
        Instance = this;

        createButton.onClick.AddListener(() => {
            LobbyManager.Instance.CreateLobby(
                lobbyNameInputField.text.Length > 0 ? lobbyNameInputField.text : "MyLobby",
                 Int32.Parse(maxPlayersInputField.text) > 0 ? Int32.Parse(maxPlayersInputField.text) : 4,
                isPrivate,
                gameMode
            );
            Hide();
        });

        publicPrivateButton.onClick.AddListener(() => {
            isPrivate = !isPrivate;
            UpdateText();
        });

        gameModeButton.onClick.AddListener(() => {
            switch (gameMode) {
                default:
                case LobbyManager.GameMode.CaptureTheFlag:
                    gameMode = LobbyManager.GameMode.Conquest;
                    break;
                case LobbyManager.GameMode.Conquest:
                    gameMode = LobbyManager.GameMode.CaptureTheFlag;
                    break;
            }
            UpdateText();
        });

        Hide();
    }
    private void UpdateText() {
        publicPrivateText.text = isPrivate ? "Private" : "Public";
        gameModeText.text = gameMode.ToString();
    }
    private void Hide() {
        gameObject.SetActive(false);
    }
    public void Show() {
        gameObject.SetActive(true);

        isPrivate = false;
        gameMode = LobbyManager.GameMode.CaptureTheFlag;

        UpdateText();
    }
}