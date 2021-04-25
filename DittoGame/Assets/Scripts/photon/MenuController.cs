using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string VersionName = "0.1";
    [SerializeField] private GameObject UsernameMenu;
    [SerializeField] private GameObject ConnectPanel;

    [SerializeField] private InputField UsernameInput;
    [SerializeField] private InputField CreateGameInput;
    [SerializeField] private InputField JoinGameInput;

    [SerializeField] private GameObject StartButton;
    private bool joinedRoom = false;
    public Text waitOpponentMsg;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(VersionName);
        waitOpponentMsg.enabled = false;
    }

    private void Start()
    {
        UsernameMenu.SetActive(true);
    }

    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected");
    }

    public void ChangeUserNameInput()
    {
        if (UsernameInput.text.Length >= 0)
        {
            StartButton.SetActive(true);
        }
        else
        {
            StartButton.SetActive(false);
        }
    }

    public void SetUserName()
    {
        UsernameMenu.SetActive(false);
        PhotonNetwork.playerName = UsernameInput.text;
    }

    public void CreateGame()
    {
        PhotonNetwork.CreateRoom(CreateGameInput.text, new RoomOptions() { maxPlayers = 5 }, null);
    }

    public void JoinGame()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.maxPlayers = 5;
        PhotonNetwork.JoinOrCreateRoom(JoinGameInput.text, roomOptions, TypedLobby.Default);
    }

    private void OnJoinedRoom()
    {
        //PhotonNetwork.LoadLevel("MainGame");
        joinedRoom = true;
        waitOpponentMsg.enabled = true;
    }

    private void Update()
    {
        Debug.Log("current room user number: " + PhotonNetwork.room.playerCount);
        if (joinedRoom == true && PhotonNetwork.room.playerCount == 2)
        {
            PhotonNetwork.LoadLevel("MainGame");
        }
    }
}
