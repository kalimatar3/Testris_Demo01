using System.Collections.Generic;
using UnityEngine;

public class ClassicGameMode : GameMode
{    
    protected static new ClassicGameMode instance;
    public static new ClassicGameMode Instance { get => instance;}
    [SerializeField] protected List<float> LevelPoint;
    public int CurLevel;
    protected bool canWin;
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) 
        {
            Debug.LogWarning("Instance already ex");
            this.gameObject.SetActive(false);
        }
        else instance = this;
    }
    public override bool CanStart()
    {
        if(GameModeManager.Instance.GameModeController.GetGameMode() == null) return false;
        if(GameModeManager.Instance.GameModeController.GetGameMode().Equals(this)) {
            return true;
        }
        return false;
    }
    public override void Init()
    {
        PanelManager.Instance.PanelController.DeActivePanel("MainMenuPanel");
        PanelManager.Instance.PanelController.SetActivePanel("ClassicMode:GamePlayPanel");
        this.transform.gameObject.SetActive(true);
        this.Point = 0;
        this.CurLevel = (int)DataManager.Instance.DynamicData.Level;
        this.PointcanGet = 10 + CurLevel * 2;
        TetrominoManager.Instance.TetrominoController.setLanded(true);
        TetrominoManager.Instance.TetrominoController.setSpeed( 2 + CurLevel* 0.5f);
    }
    public override void Playing() {
        TetrominoManager.Instance.SpawnTetromino.spawnTetromino();
        TetrominoManager.Instance.TetrominoController.Main();
        if(Point >= LevelPoint[CurLevel]) {
            Point = 0;
            canWin = true;
        }
    }
    public override bool CanLose()
    {
       foreach (Transform element in BoardManager.Instance.Board.GetXZ_2D_Array(21)) {
        if(element != null && element.gameObject.activeInHierarchy) {
            return true;
        }
       }
       return false;
    }
    public override void Lose()
    {
        Debug.Log("LOSE");
        InputManager.Instance.touchcontrols.Disable();
        PanelManager.Instance.PanelController.SetActivePanel("LosePanel");
    }

    public override bool CanWin()
    {
        return canWin;
    }
    public override void Win()
    {
        canWin = false;
        Debug.Log("WIN");
        InputManager.Instance.touchcontrols.Disable();
        PanelManager.Instance.PanelController.SetActivePanel("ClassicMode:WinPanel");
    }
}
