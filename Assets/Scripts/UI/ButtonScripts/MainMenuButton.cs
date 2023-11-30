using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : BaseButton
{
    protected override void Act()
    { 
        foreach(Transform element in PanelManager.Instance.GetListPanel()) {
            element.gameObject.SetActive(false);
        }
        PanelManager.Instance.PanelController.SetActivePanel("MainMenuPanel");
    }
}
