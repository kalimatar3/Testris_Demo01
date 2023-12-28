using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGsettingbutton : BaseButton
{
    protected override void Act() {
        base.Act();
        if(PanelManager.Instance.PanelController.getpanel("SettingPanel") == null) return;
        if(PanelManager.Instance.PanelController.getpanel("SettingPanel").gameObject.activeInHierarchy) {
            PanelManager.Instance.PanelController.DeActivePanel("SettingPanel");
        }
        else {
            PanelManager.Instance.PanelController.SetActivePanel("SettingPanel");
        }
    }
}
