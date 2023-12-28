using UnityEngine;
using System;
using System.Collections;
using Photon.Realtime;
using Photon.Pun;
public class PhotonManager : MyBehaviour
{
    protected static PhotonManager instance;
    public static PhotonManager Instance { get => instance ;}
    public string PlayerID {get; private set;}
    [SerializeField] protected PhotonLog photonLog;
    public PhotonLog PhotonLog => photonLog;
    [SerializeField] protected PhotonRoom photonRoom;
    public PhotonRoom PhotonRoom => photonRoom;
    protected override void Start() {
        base.Start();
        StartCoroutine(StartDelay());
    }
    protected IEnumerator StartDelay() {
        PanelManager.Instance.PanelController.SetActivePanel("LoadingPanel");
        PhotonManager.Instance.PhotonLog.Login();
        yield return new WaitUntil(predicate: ()=> {
            if(PhotonNetwork.NetworkClientState != ClientState.ConnectedToMasterServer) return false;
            return true;
        });
        yield return new WaitForSeconds(0.5f);
        PanelManager.Instance.PanelController.DeActivePanel("LoadingPanel");
    }
    protected void LoadPhoton() {
        foreach(Transform element in this.transform) {
            if(element.GetComponent<PhotonLog>()) photonLog = element.GetComponent<PhotonLog>();
            if(element.GetComponent<PhotonRoom>()) photonRoom = element.GetComponent<PhotonRoom>();
        }
    }
    protected override void LoadComponents() {
        base.LoadComponents();
        this.LoadPhoton();
        this.LoadPlayerID();
    }
    protected void LoadPlayerID() {
        Guid playerGuid = Guid.NewGuid();       
        string PlayerID = playerGuid.ToString();
    }
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }
}
