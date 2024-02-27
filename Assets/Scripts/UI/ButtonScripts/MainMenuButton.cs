using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuButton : BaseButton
{
    protected override void Act()
    { 
        base.Act();
        OnlineGameMode.Instance.Players[1].GetComponent<PhotonView>().RPC("SetWin",RpcTarget.All);
        if(PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();
        if(PhotonNetwork.InLobby) PhotonNetwork.LeaveLobby();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  
        Lsmanager.Instance.SaveGame();
    }
}
