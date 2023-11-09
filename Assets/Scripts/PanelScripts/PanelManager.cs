using System.Collections.Generic;
using UnityEngine;
public class PanelManager : MyBehaviour
{
    protected static PanelManager instance;
    public static PanelManager Instance { get => instance;}
    public PanelController PanelController => panelController;
    protected PanelController panelController {get;private set;}
    [SerializeField] protected List<Transform> ListPanel;
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }
    protected override void LoadComponents() {
        base.LoadComponents();
        this.LoadListPanel();
        this.LoadPanelController();
    }
    protected void LoadPanelController() {
        this.panelController = GetComponent<PanelController>();
    }
    protected void LoadListPanel() {
        if(ListPanel.Count >0) return;
        foreach(Transform element in this.transform) {
            ListPanel.Add(element);
        }
    }
    public List<Transform> GetListPanel() {
        return this.ListPanel;
    }
}
