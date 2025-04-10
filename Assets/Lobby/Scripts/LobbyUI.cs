using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour 
{
    public static LobbyUI Instance { get; private set; }
    [SerializeField] private Transform playerSingleTemplate;
    [SerializeField] private Transform container;
    [SerializeField] private TextMeshProUGUI lobbyNameText;
    [SerializeField] private TextMeshProUGUI playerCountText;
    [SerializeField] private TextMeshProUGUI gameModeText;
    [SerializeField] private Button leaveLobbyButton;
    [SerializeField] private Button changeGameModeButton;
    [SerializeField] private Button startGameButton;
    [SerializeField] private TextMeshProUGUI lobbyCode;


    private void Awake() {
        Instance = this;
        playerSingleTemplate.gameObject.SetActive(false);

        leaveLobbyButton.onClick.AddListener(() => {
            LobbyManager.Instance.LeaveLobby();
        });
        //changeGameModeButton.onClick.AddListener(() => {
        //    LobbyManager.Instance.ChangeGameMode();
        //});
        startGameButton.onClick.AddListener(() =>
        {
            LobbyManager.Instance.StartGame();
        });
    }

    private void Start() {
        LobbyManager.Instance.OnJoinedLobby += UpdateLobby_Event;
        LobbyManager.Instance.OnJoinedLobby += SetLobbyCodeText;
        LobbyManager.Instance.OnJoinedLobbyUpdate += UpdateLobby_Event;
        LobbyManager.Instance.OnLobbyGameModeChanged += UpdateLobby_Event;
        LobbyManager.Instance.OnLeftLobby += LobbyManager_OnLeftLobby;
        LobbyManager.Instance.OnKickedFromLobby += LobbyManager_OnLeftLobby;

        Hide();
    }

    private void LobbyManager_OnLeftLobby(object sender, System.EventArgs e) {
        ClearLobby();
        Hide();
    }

    private void UpdateLobby_Event(object sender, LobbyManager.LobbyEventArgs e) {
        UpdateLobby();
    }

    private void UpdateLobby() {
        UpdateLobby(LobbyManager.Instance.GetJoinedLobby());
    }
    private void SetLobbyCodeText(object sender, LobbyManager.LobbyEventArgs e)
    {
        lobbyCode.text = LobbyManager.Instance.GetJoinedLobby().LobbyCode.ToString();
    }

    private void UpdateLobby(Lobby lobby) {
        ClearLobby();

        int playerCount = 0;
        foreach (Player player in lobby.Players) 
        {
            Transform playerSingleTransform = Instantiate(playerSingleTemplate, container);
            playerSingleTransform.gameObject.SetActive(true);
            LobbyPlayerSingleUI lobbyPlayerSingleUI = playerSingleTransform.GetComponent<LobbyPlayerSingleUI>();


            lobbyPlayerSingleUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(lobbyPlayerSingleUI.GetComponent<RectTransform>().anchoredPosition.y,
                playerSingleTemplate.GetComponent<RectTransform>().anchoredPosition.y - playerCount * playerSingleTemplate.GetComponent<RectTransform>().sizeDelta.y);
            playerCount++;
            lobbyPlayerSingleUI.SetKickPlayerButtonVisible(
                LobbyManager.Instance.IsLobbyHost() &&
                player.Id != AuthenticationService.Instance.PlayerId // Don't allow kick yourself
            );

            lobbyPlayerSingleUI.UpdatePlayer(player);
        }

        //changeGameModeButton.gameObject.SetActive(LobbyManager.Instance.IsLobbyHost());
        startGameButton.gameObject.SetActive(LobbyManager.Instance.IsLobbyHost());

        lobbyNameText.text = lobby.Name;
        playerCountText.text = lobby.Players.Count + "/" + lobby.MaxPlayers;
        gameModeText.text = lobby.Data[LobbyManager.KEY_GAME_MODE].Value;

        Show();
    }
    private void ClearLobby()
    {
        foreach (Transform child in container) {
            if (child == playerSingleTemplate) continue;
            Destroy(child.gameObject);
        }
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }

}