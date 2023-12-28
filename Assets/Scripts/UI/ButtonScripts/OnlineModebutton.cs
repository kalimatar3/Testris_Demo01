using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnlineModebutton : BaseButton
{
    [SerializeField] protected Transform onlinebutton,selectgamemode;
    protected override void Act() {
        base.Act();
        SceneManager.LoadScene("OnlineScene");  
    }
    protected IEnumerator ActDeay() {
        PanelManager.Instance.PanelController.SetActivePanel("LoadingPanel");
        yield return new WaitUntil(predicate: ()=> {
            if(PhotonNetwork.NetworkClientState != ClientState.PeerCreated) return false;
            return true;
        });
        PhotonManager.Instance.PhotonLog.Login();
        yield return new WaitUntil(predicate: ()=> {
            if(PhotonNetwork.NetworkClientState != ClientState.ConnectedToMasterServer) return false;
            return true;
        });
        yield return new WaitForSeconds(0.5f);
        PanelManager.Instance.PanelController.DeActivePanel("LoadingPanel");
        this.selectgamemode.gameObject.SetActive(false);
        this.onlinebutton.gameObject.SetActive(true);
    }
}
