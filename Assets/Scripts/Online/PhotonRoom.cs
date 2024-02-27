using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonRoom : MonoBehaviourPunCallbacks
{
    public Transform CameraUI;
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
        this.CameraUI.gameObject.SetActive(false);
        if(PhotonNetwork.IsMasterClient) {  
            GameObject Player1 = PhotonNetwork.Instantiate("Player1Online",Vector3.zero,Quaternion.identity);
            Player1.GetComponent<PlayerOnlineSetup>().IslocalPlayer();
        }
        else {
            Quaternion rotaion = Quaternion.Euler(180,0,0);
            Vector3 position  = new Vector3(0,-1,9);
            GameObject Player2 = PhotonNetwork.Instantiate("Player2Online",position,rotaion);
            Player2.GetComponent<PlayerOnlineSetup>().IslocalPlayer();
        }
    }
    public override void OnLeftRoom() {
        this.CameraUI.gameObject.SetActive(true);
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
