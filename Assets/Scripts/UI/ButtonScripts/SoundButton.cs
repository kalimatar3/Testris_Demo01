using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton : BaseButton
{
    protected override void Act() {
        base.Act();
        PanelManager.Instance.PanelController.SetActivePanel("SoundSettingPanel");
    }
}
