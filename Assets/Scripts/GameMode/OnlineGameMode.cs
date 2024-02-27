using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine;
public class OnlineGameMode : GameMode
{    
    protected static new OnlineGameMode instance;
    public static new OnlineGameMode Instance { get => instance;}
    public TetrominoManagerOnline TetrominoManagerOnline;
    public BoardManagerOnline BoardManagerOnline;
    public  GameObject[] Players = new GameObject[2];
    [SerializeField] protected float PlayTime;
    public float WaitingTime;
    public float GetTime() {
        return this.PlayTime;
    }
    public float GetWaitingTime() {
        return this.WaitingTime;
    }
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) this.gameObject.SetActive(false);
        else instance = this;
    }
    public override bool CanStart()
    {
        if(GameModeManager.Instance.GameModeController.GetGameMode() == null) return false;
        if(TetrominoManagerOnline.Instance == null) return false;
        if(BoardManagerOnline.Instance == null) return false;
        if(GameModeManager.Instance.GameModeController.GetGameMode().Equals(this)) {
            Debug.Log("Starting " + this.transform.name);
            return true;
        }
        return false;
    }
    public override bool CanLose()
    {
       foreach (Transform element in BoardManagerOnline.Board.GetXZ_2D_Array(Players[0].GetComponent<PlayerOnline>().Hp + 1)) {
        if(element != null && element.gameObject.activeInHierarchy) {
            return true;
        }
       }
       return false;
    }
    public override bool CanWin()
    {
        return Players[0].GetComponent<PlayerOnline>().win;
    }
    public override void Init()
    {
        this.transform.gameObject.SetActive(true);
        TetrominoManagerOnline = TetrominoManagerOnline.Instance;
        BoardManagerOnline = BoardManagerOnline.Instance;
        PanelManager.Instance.PanelController.DeActivePanel("MainMenuPanel");
        PanelManager.Instance.PanelController.SetActivePanel("OnlineGameMode:GamePlayPanel");
        this.Point = 0;
        this.PointcanGet = 10;
        this.PlayTime = 0;
        TetrominoManagerOnline.TetrominoController.setLanded(true);
        TetrominoManagerOnline.TetrominoController.setSpeed(2);
    }
    public override void Lose()
    {
        Debug.Log("LOSE");
        Players[1].GetComponent<PhotonView>().RPC("SetWin",RpcTarget.All);
        if(PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();
        if(PhotonNetwork.InLobby) PhotonNetwork.LeaveLobby();
        PanelManager.Instance.PanelController.SetActivePanel("LosePanel");
        InputManager.Instance.touchcontrols.Disable();
    }
    public override void Win()
    {
        Debug.Log("WIN");
        if(PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();
        if(PhotonNetwork.InLobby) PhotonNetwork.LeaveLobby();
        PanelManager.Instance.PanelController.SetActivePanel("WinPanel");
        InputManager.Instance.touchcontrols.Disable();
    }
    public override void Playing()
    {
        PhotonView[] photonViews = PhotonNetwork.PhotonViews;
        foreach(PhotonView ele in photonViews) {
           if(ele.IsMine && ele.GetComponent<PlayerOnline>() != null) {
                Players[0] = ele.gameObject;
           }
           else if(!ele.IsMine && ele.GetComponent<PlayerOnline>() != null) {
                Players[1] = ele.gameObject;
           }
        }
        TetrominoManagerOnline.SpawnTetrominoOnline.spawnTetromino();
        TetrominoManagerOnline.TetrominoController.Main();
        PlayTime += Time.deltaTime *1f;
        TetrominoManagerOnline.TetrominoController.setSpeed(1 + (int)PlayTime/(int)30);
    }
        public override IEnumerator Play() {
        yield return new WaitUntil(predicate:() => {
            return this.CanStart();
        });
        this.Init();
        while(true) {
            this.Playing();
            if(CanLose()) {
                this.Lose();
                BoardManagerOnline.Board.ClearAllMatrix();
                break;
            }
            if(CanWin()) {
                this.Win();
                BoardManagerOnline.Board.ClearAllMatrix();
                break;
            }
            yield return new WaitForSecondsRealtime(0f);
        }
    }

}
