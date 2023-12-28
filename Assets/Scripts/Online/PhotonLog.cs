using Photon.Pun;
using UnityEngine;
public class PhotonLog : MonoBehaviourPunCallbacks
{
    public virtual void Login() {
        string name = "Player";
        Debug.Log(transform.name + " : login " + name);
        PhotonNetwork.LocalPlayer.NickName = name;
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnJoinedLobby() {
        Debug.Log("JoinedLobby");
    }
    public override void OnConnectedToMaster() {
        Debug.Log("OnConnectedToMaster");
    }
    public override void OnLeftLobby() {
        Debug.Log("LeftLobby");
    }
}
