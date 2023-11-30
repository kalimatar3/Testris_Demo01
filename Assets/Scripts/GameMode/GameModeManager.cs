using System.Collections.Generic;
using UnityEngine;
public class GameModeManager : MyBehaviour
{    
    protected static GameModeManager instance;
    public static GameModeManager Instance { get => instance;}
    [SerializeField] protected GameModeController gameModeController {get ; private set;}
    public GameModeController GameModeController => gameModeController;
    [SerializeField] protected List<Transform> ListgameMode;
    protected override void LoadComponents() {
        base.LoadComponents();
        this.LoadListGameMode();
        this.LoadgameModeComtroller();
    }
    protected void LoadgameModeComtroller() {
        this.gameModeController = GetComponent<GameModeController>();
    }
    protected void LoadListGameMode() {
        if(ListgameMode.Count > 0 ) return;
        foreach(Transform element in this.transform) {
            GameMode thisgameMode = element.GetComponent<GameMode>();
            if(element.gameObject.activeInHierarchy && thisgameMode != null) {
                this.ListgameMode.Add(element);
            }
        }
    }
    public List<Transform> GetListgameMode() {
        return ListgameMode;
    }
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }
}
