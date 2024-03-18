using System.Collections;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using System.Runtime.InteropServices.WindowsRuntime;

public class PhotonRoom : MonoBehaviourPunCallbacks
{
    public Transform CameraUI;
    public float WaitingTime;
    public float minute = 0,seconds;
    public RoomOptions roomOptions = new RoomOptions
    {
        MaxPlayers = 2, 
        IsVisible = true, 
        IsOpen = true 
    };
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
        StartCoroutine(this.IEOnJoinedRoom());
    }
    public IEnumerator IEOnJoinedRoom() {
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
        // Tweener rotate = BoardManagerOnline.Instance.Model.transform.DOLocalRotate(new Vector3(0,360f,0),3f, RotateMode.FastBeyond360)
        // .SetLoops(-1,LoopType.Restart)
        // .SetEase(Ease.Linear);
        yield return new WaitUntil(predicate:() =>{
        if(!PhotonNetwork.InRoom) return true;
        if(!CanPlay()) {
            this.OnWaitingRoom();
            return false;
        }       
        else 
        {
            this.OnWaitingRoom();
            return true;
        }
        });
        if(PhotonNetwork.InRoom) {
//            rotate.Kill();
            BoardManagerOnline.Instance.Model.transform.DOLocalRotate(new Vector3(0,0,0),0.1f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear);        
            this.WaitingTime = 0;
            this.seconds = 0;
            this.minute = 0;
            yield return new WaitForSeconds(1f);
            PanelManager.Instance.PanelController.SetActivePanel("3");
            yield return new WaitForSeconds(1f);
            PanelManager.Instance.PanelController.SetActivePanel("2");
            yield return new WaitForSeconds(1f);
            PanelManager.Instance.PanelController.SetActivePanel("1");
            yield return new WaitForSeconds(1f);
            PanelManager.Instance.PanelController.SetActivePanel("Fight");
            yield return new WaitForSeconds(1f);
            Debug.Log("Playonline");
            PanelManager.Instance.PanelController.DeActivePanel("MainMenuPanel");
            GameModeManager.Instance.GameModeController.SetGameMode("Online");
        }
    }
    public bool CanPlay() {
        if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.PlayerCount < this.roomOptions.MaxPlayers) return false; 
        else return true;
    }
    public void OnWaitingRoom() {
        Debug.Log("OnWaitingRoom");
        Quaternion newrotation = Quaternion.Euler(BoardManagerOnline.Instance.Model.transform.localRotation.eulerAngles + new Vector3(0,180f*(Time.deltaTime*1f),0));
        BoardManagerOnline.Instance.Model.transform.localRotation = newrotation;
        PanelManager.Instance.PanelController.SetActivePanel("CancelGameButton");
        PanelManager.Instance.PanelController.DeActivePanel("FindGameButton");
        PanelManager.Instance.transform.Find("MainMenuPanel").Find("BackGround").gameObject.SetActive(false);
        PanelManager.Instance.transform.Find("MainMenuPanel").Find("GameName").gameObject.SetActive(false);
        PanelManager.Instance.PanelController.SetActivePanel("WaitTime");
        seconds += Time.deltaTime *1f; 
        if(seconds >= 60f) {
            minute ++;
            seconds = 0;
        }
    }
    public override void OnLeftRoom() {
        this.WaitingTime = 0;
        this.seconds = 0;
        this.minute = 0;
        this.CameraUI.gameObject.SetActive(true);
        PanelManager.Instance.PanelController.DeActivePanel("CancelGameButton");
        PanelManager.Instance.PanelController.SetActivePanel("FindGameButton");
        PanelManager.Instance.transform.Find("MainMenuPanel").Find("BackGround").gameObject.SetActive(true);
        PanelManager.Instance.transform.Find("MainMenuPanel").Find("GameName").gameObject.SetActive(true);
        PanelManager.Instance.PanelController.DeActivePanel("WaitTime");
        Debug.Log("OnLeftRoom");
    }
    public void TryJoinOrCreateRoom(string name)
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No room found, creating a new room.");
        PhotonNetwork.CreateRoom(PhotonManager.Instance.PlayerID, roomOptions); 
    }
 }
