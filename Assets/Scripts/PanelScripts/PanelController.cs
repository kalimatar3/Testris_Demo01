using System;
using UnityEngine;

public class PanelController : MyBehaviour
{
    [SerializeField] protected PanelManager panelManager {get; private set;}
    public PanelManager PanelManager => panelManager;
    protected override void LoadComponents() {
        base.LoadComponents();
        this.panelManager = GetComponent<PanelManager>();
        if(panelManager == null) Debug.LogWarning("Cant found PanelManager");
    }
    public void SetActivePanel(String Panelname) {
        if(!IsPanelValid(Panelname)) return;
        foreach(Transform element in panelManager.GetListPanel()) {
            if(element.name == Panelname) {
                element.gameObject.SetActive(true);
            }
        }
    }
    public void DeActivePanel(String Panelname) {
        if(!IsPanelValid(Panelname)) return;
        foreach(Transform element in panelManager.GetListPanel()) {
            if(element.name == Panelname) {
                element.gameObject.SetActive(false);
            }
        }
    }
    protected bool IsPanelValid(String name) {
        foreach(Transform element in panelManager.GetListPanel()) {
            if(element.name == name) return true; 
        }
        Debug.LogWarning(" Cant Found " + name);
        return false;
    }
}
