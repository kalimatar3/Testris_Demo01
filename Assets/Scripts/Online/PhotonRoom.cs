using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonRoom : MonoBehaviourPunCallbacks
{
    public GameObject PlayerOnline; 
    public virtual void Createroom(string name) {
        Debug.Log(transform.name + " : create Room " + name);
        PhotonNetwork.CreateRoom(name);
    }
    public virtual void Join(string name) {
        Debug.Log("joined Room " + name);
        PhotonNetwork.JoinRoom(name);
    }
    public override void OnCreatedRoom() {
        Debug.Log("OncreatedRoom");
    }
    public override void OnJoinedRoom() {     
        Debug.Log("OnjoinedRoom");
         if (PhotonNetwork.IsMasterClient) {
            GameObject player = PhotonNetwork.Instantiate(PlayerOnline.name, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
            player.GetComponent<PlayerOnlineSetup>().IslocalPlayer();
         }
         else {
            GameObject player = PhotonNetwork.Instantiate(PlayerOnline.name, new Vector3(0, -1, 9), Quaternion.Euler(180f, 0, 0));
            player.GetComponent<PlayerOnlineSetup>().IslocalPlayer();
         }
    }
    public override void OnLeftRoom() {
        PhotonView[] photonViews = PhotonNetwork.PhotonViews;
        foreach(PhotonView ele in photonViews) {
            if(!ele.IsMine && ele.GetComponent<PlayerOnline>() != null ) ele.RPC("SetWin",RpcTarget.All);
        }
        Debug.Log("OnLeftRoom");
    }
    public void TryJoinOrCreateRoom(string name)
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No room found, creating a new room.");

        RoomOptions roomOptions = new RoomOptions
        {
            MaxPlayers = 2, 
            IsVisible = true, 
            IsOpen = true 
        };
        PhotonNetwork.CreateRoom(PhotonManager.Instance.PlayerID, roomOptions); 
    }
 }
