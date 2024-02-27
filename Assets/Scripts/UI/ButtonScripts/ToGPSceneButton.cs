using UnityEngine.SceneManagement;
using Photon.Pun;

public class ToGPSceneButton : BaseButton
{
    protected override void Act() {
        base.Act();
        if(PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();
        if(PhotonNetwork.InLobby) PhotonNetwork.LeaveLobby();
        SceneManager.LoadScene("GamePlay");  
    }
}
