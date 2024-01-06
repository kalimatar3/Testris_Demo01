using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLessGameMode : GameMode
{
    protected static new EndLessGameMode instance;
    public static new EndLessGameMode Instance { get => instance;}
    [SerializeField] protected float PlayTime;
    public float GetTime() {
        return this.PlayTime;
    }
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) this.gameObject.SetActive(false);
        else instance = this;
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
    public override bool CanStart()
    {
        if(GameModeManager.Instance.GameModeController.GetGameMode() == null) return false;
        if(GameModeManager.Instance.GameModeController.GetGameMode().Equals(this)) {
            Debug.Log("Starting " + this.transform.name);
            return true;
        }
        return false;
    }
    public override bool CanWin()
    {
       return false;
    }

    public override void Init()
    {
        PanelManager.Instance.PanelController.DeActivePanel("MainMenuPanel");
        PanelManager.Instance.PanelController.SetActivePanel("EndLessGameMode:GamePlayPanel");
        this.transform.gameObject.SetActive(true);
        this.Point = 0;
        this.PointcanGet = 10;
        this.PlayTime = 0;
        TetrominoManager.Instance.TetrominoController.setLanded(true);
        TetrominoManager.Instance.TetrominoController.setSpeed(2);
        if(!DataManager.Instance.DynamicData.Tutorial) {
        DataManager.Instance.DynamicData.Tutorial = true;
        StartCoroutine(this.Tutorial());
        Lsmanager.Instance.SaveGame();
        }
    }

    public override void Lose()
    {
        Debug.Log("LOSE");
        InputManager.Instance.touchcontrols.Disable();
        PanelManager.Instance.PanelController.SetActivePanel("LosePanel");
    }

    public override void Playing()
    {
        TetrominoManager.Instance.SpawnTetromino.spawnTetromino();
        TetrominoManager.Instance.TetrominoController.Main();
        PlayTime += Time.deltaTime *1f;
    }
    public virtual IEnumerator Tutorial() {
        yield return new WaitForSeconds(1f);
        PanelManager.Instance.PanelController.SetActivePanel("TutorialPanel1");
        Time.timeScale = 0f;
        yield return new WaitUntil(predicate:() => {
            if(InputManager.Instance.touchcontrols.Touch.TouchPress.IsPressed()) return true;
            return false;
        });
        Time.timeScale = 1f;
        PanelManager.Instance.PanelController.DeActivePanel("TutorialPanel1");

        yield return new WaitForSeconds(2f);
        PanelManager.Instance.PanelController.SetActivePanel("TutorialPanel2");
        Time.timeScale = 0f;
        yield return new WaitUntil(predicate:() => {
            if(InputManager.Instance.touchcontrols.Touch.TouchPress.IsPressed()) return true;
            return false;
        });
        Time.timeScale = 1f;
        PanelManager.Instance.PanelController.DeActivePanel("TutorialPanel2");

        yield return new WaitForSeconds(2f);
        PanelManager.Instance.PanelController.SetActivePanel("TutorialPanel3");
        Time.timeScale = 0f;
        yield return new WaitUntil(predicate:() => {
            if(InputManager.Instance.touchcontrols.Touch.TouchPress.IsPressed()) return true;
            return false;
        });
        Time.timeScale = 1f;
        PanelManager.Instance.PanelController.DeActivePanel("TutorialPanel3");

    }

    public override void Win()
    {
        InputManager.Instance.touchcontrols.Disable();
        Debug.Log("WIN");
    }
}
