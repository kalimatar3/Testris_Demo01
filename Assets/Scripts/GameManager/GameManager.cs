using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MyBehaviour
{
    protected static GameManager instance;
    public static GameManager Instance { get => instance;}
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }
    protected void StartPlay() {
        Debug.Log("START");
        TetrominoManager.Instance.SpawnTetromino.canSpawn = true;
    }
    protected void Win() {
        Debug.Log("WIN");
        // win game behaviour;
    }
    protected void Lose() {
        Debug.Log("LOSE");
        //lose game behaviour;
    }
    public void EndGame(IGameMode gameMode) {
        if(gameMode == null) return;
        if(gameMode.CanLose()) this.Lose();
        if(gameMode.CanWin()) this.Win();
    }
    protected override void Start() {
        base.Start();
        StartCoroutine(StartPlayDelay());
    }
    protected IEnumerator StartPlayDelay() {
        yield return new WaitUntil(predicate:() => {
            if(GameModeManager.Instance.GameModeController.GetCurrentGameMode() == null) return false;
            return GameModeManager.Instance.GameModeController.GetCurrentGameMode().CanStart();
        });
        this.StartPlay();
    }
    protected void Update() {
        this.EndGame(GameModeManager.Instance.GameModeController.GetCurrentGameMode());
    } 
}
