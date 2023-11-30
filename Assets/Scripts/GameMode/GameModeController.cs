using System;
using UnityEngine;

public class GameModeController : MyBehaviour
{
    [SerializeField] protected GameModeManager gameModeManager {get;private set;}
    public GameModeManager GameModeManager  => gameModeManager;
    protected GameMode CurrentGameMode;
    protected override void LoadComponents() {
        base.LoadComponents();
        this.LoadGameModeManager();
    }
    protected void LoadGameModeManager() {
        this.gameModeManager = GetComponent<GameModeManager>();
    }
    public void SetGameMode(string Gamemodename) {
        foreach(Transform element in gameModeManager.GetListgameMode()) {
            element.gameObject.SetActive(false);
            if(element.name == Gamemodename) {
                this.CurrentGameMode= element.GetComponent<GameMode>();
                element.gameObject.SetActive(true);
                StartCoroutine(CurrentGameMode.Play());
            }
        }
    }
    public GameMode GetGameMode() {
        return CurrentGameMode;
    }
}
