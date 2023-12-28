using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
public class OnlineGameMode : GameMode
{    
    protected static new OnlineGameMode instance;
    public static new OnlineGameMode Instance { get => instance;}
    public TetrominoManagerInOnline TetrominoManagerInOnline;
    public BoardManagerOnline BoardManagerOnline;
    [SerializeField] protected float PlayTime;
    public float WaitingTime;
    public bool win;
    public int Hp = 10;
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
        if(GameModeManager.Instance.GameModeController.GetGameMode().Equals(this)) {
            Debug.Log("Starting " + this.transform.name);
            return true;
        }
        return false;
    }
    public override bool CanLose()
    {
       foreach (Transform element in BoardManagerOnline.Board.GetXZ_2D_Array(Hp + 1)) {
        if(element != null && element.gameObject.activeInHierarchy) {
            return true;
        }
       }
       return false;
    }
    public override bool CanWin()
    {
        return win;
    }
    public override void Init()
    {
        PanelManager.Instance.PanelController.DeActivePanel("MainMenuPanel");
        PanelManager.Instance.PanelController.SetActivePanel("OnlineGameMode:GamePlayPanel");
        this.transform.gameObject.SetActive(true);
        this.Point = 0;
        this.PointcanGet = 10;
        this.PlayTime = 0;
        TetrominoManagerInOnline.TetrominoController.setLanded(true);
        TetrominoManagerInOnline.TetrominoController.setSpeed(2);
    }

    public override void Lose()
    {
        Debug.Log("LOSE");
        PhotonView[] photonViews = PhotonNetwork.PhotonViews;
        foreach(PhotonView ele in photonViews) {
            if(!ele.IsMine && ele.GetComponent<PlayerOnline>() != null ) ele.RPC("SetWin",RpcTarget.All);
        }
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
        TetrominoManagerInOnline.SpawnTetromino.spawnTetromino();
        TetrominoManagerInOnline.TetrominoController.Main();
        PlayTime += Time.deltaTime *1f;
        TetrominoManagerInOnline.TetrominoController.setSpeed(0.5f + (int)PlayTime/(int)10);
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
