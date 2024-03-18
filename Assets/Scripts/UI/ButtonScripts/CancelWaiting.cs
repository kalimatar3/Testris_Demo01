using System.Collections;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class CancelWaiting : BaseButton
{    
    protected override void Act() {
        base.Act();
        if(PhotonNetwork.IsConnected) {
            PhotonNetwork.LeaveRoom();
        }
    }
}
