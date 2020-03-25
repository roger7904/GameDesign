using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerManager : MonoBehaviourPunCallbacks,IPunObservable
{
    public float health = 1f;
    [SerializeField]
    private Text playerNameText;
    // [Tooltip("指標- GameObject PlayerUI")]
    // [SerializeField]
    // public GameObject PlayerUIPrefab;
    // Start is called before the first frame update
    void Start()
    {
        // if (PlayerUIPrefab != null){
        //     GameObject _uiGo = Instantiate(PlayerUIPrefab);
        //     _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        // }
        // else{
        //     Debug.LogWarning("指標- GameObject PlayerUI 為空值", this);
        // }
        
        //playerNameText.text=this.photonView.Owner.NickName;
        playerNameText.text=PhotonNetwork.NickName;
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        if (stream.IsWriting){
            // 為玩家本人的狀態, 將 health 的狀態更新給其他玩家
            stream.SendNext(health);
        }
        else{
            // 非為玩家本人的狀態, 單純接收更新的資料
            this.health = (float)stream.ReceiveNext();
        }
    }
    void CalledOnLevelWasLoaded(){
        // if (PlayerUIPrefab != null){
        //     GameObject _uiGo = Instantiate(PlayerUIPrefab);
        //     _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        // }
        // else{
        //     Debug.LogWarning("指標- GameObject PlayerUI 為空值", this);
        // }
    }
}
