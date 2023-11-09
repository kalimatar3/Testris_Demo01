using UnityEngine;

public class GameModeController : MyBehaviour
{
    [SerializeField] protected GameModeManager gameModeManager {get;private set;}
    public GameModeManager GameModeManager  => gameModeManager;
    [SerializeField] protected IGameMode CurrentGameMode = null;
    protected override void LoadComponents() {
        base.LoadComponents();
        this.LoadGameModeManager();
    }
    protected void LoadGameModeManager() {
        this.gameModeManager = GetComponent<GameModeManager>();
    }
    public IGameMode GetCurrentGameMode() {
        if(CurrentGameMode == null) return null;
        return CurrentGameMode;
    } 
    public void SetGameMode(string Gamemodename) {
        foreach(Transform element in gameModeManager.GetListgameMode()) {
            if(element.name == Gamemodename) {
                CurrentGameMode = element.GetComponent<IGameMode>();
                return;
            }
        }
        Debug.Log("Cant found " + Gamemodename + " GameMode");
    }
}
