using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class HomepageManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private byte maxPlayersPerRoom = 4;
    [SerializeField]
    private GameObject controlPanel;
    [SerializeField]
    private GameObject progressLabel;

    private string gameVersion = "v0.0.1";
    private bool isConnecting;
    private string nameString;

    void Awake(){
        // 確保所有連線的玩家均載入相同的遊戲場景
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start(){
        //Connect();
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
        PhotonNetwork.NickName = PlayerPrefs.GetString("playerName");
    }

    public void Search(){
        isConnecting = true;
        progressLabel.SetActive(true);
        controlPanel.SetActive(false);
        // 檢查是否與 Photon Cloud 連線
        if (PhotonNetwork.IsConnected){
        // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
            PhotonNetwork.JoinRandomRoom();
        }
        else{
            // 未連線, 嘗試與 Photon Cloud 連線
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    public override void OnConnectedToMaster(){
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
        // 確認已連上 Photon Cloud
        Debug.Log("呼叫OnConnectedToMaster(), 已連上 Photon Cloud");
        // 隨機加入一個遊戲室
        if (isConnecting){
            PhotonNetwork.JoinRandomRoom();
        }
    }
    public override void OnDisconnected(DisconnectCause cause){
        Debug.LogWarningFormat("呼叫 OnDisconnected() {0}.", cause);
    }
    public override void OnJoinRandomFailed(short returnCode, string message){ 
        // 隨機加入遊戲室失敗. 可能原因是 1. 沒有遊戲室 或 2. 有的都滿了.
        Debug.Log("呼叫OnJoinRandomFailed(), 隨機加入遊戲室失敗.");
        // 自己開一個遊戲室.
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        //PhotonNetwork.CreateRoom(null, new RoomOptions());
    }
    public override void OnJoinedRoom(){
        Debug.Log("呼叫 OnJoinedRoom(), 已成功進入遊戲室中.");
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1){
            Debug.Log("第一個玩家");
            PhotonNetwork.LoadLevel("PrepareRoom");
        }
    }
    // void OnJoinedLobby(){
    //     RoomOptions roomOptions=new RoomOptions(){ IsVisible=false,MaxPlayers=4};
    //     PhotonNetwork.JoinOrCreateRoom(roomName,roomOptions,TypedLobby.Default);
    // }

    // void OnJoinedRoom(){
    //     PhotonNetwork.Instantiate(playerPrefName,spawnPoint.position,spawnPoint.rotation,0);
    // }
   
}
