using System.Collections;
using Photon.Pun;
using UnityEngine;

public class FindorCreateRoombutton : BaseButton
{
    public float WaitingTime;
    public float minute = 0,seconds;
    protected override void Act() {
        base.Act();
        if(PhotonNetwork.IsConnected) {
            StartCoroutine(this.ActDeay());
        }
    }
    protected IEnumerator ActDeay() {
        PhotonManager.Instance.PhotonRoom.TryJoinOrCreateRoom(PhotonManager.Instance.PlayerID);
        yield return new WaitUntil(predicate: ()=> {
            if ( PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.PlayerCount < 2 ) 
            {
                seconds += Time.deltaTime *1f; 
                if(seconds >= 60f) {
                    minute ++;
                    seconds = 0;
                }
                return false;
            }
            else 
            {
                WaitingTime = 0;
                return true;
            }
        });
        GameModeManager.Instance.GameModeController.SetGameMode("Online");
        Debug.Log("Playonline");
        PanelManager.Instance.PanelController.DeActivePanel("MainMenuPanel");
    }
}
