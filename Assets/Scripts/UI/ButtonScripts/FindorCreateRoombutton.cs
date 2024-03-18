using System.Collections;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class FindorCreateRoombutton : BaseButton
{
    protected override void Act() {
        base.Act();
        if(PhotonNetwork.IsConnected) {
            if(!PhotonNetwork.InRoom) PhotonManager.Instance.PhotonRoom.TryJoinOrCreateRoom(PhotonManager.Instance.PlayerID);
            else PhotonNetwork.LeaveRoom();
        }
    }
}
