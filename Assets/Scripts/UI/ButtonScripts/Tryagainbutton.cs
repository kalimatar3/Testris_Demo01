using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tryagainbutton : BaseButton
{
    protected override void Act()
    {
        base.Act();
       PanelManager.Instance.PanelController.DeActivePanel("LosePanel");
       GameModeManager.Instance.GameModeController.SetGameMode(GameModeManager.Instance.GameModeController.GetGameMode().transform.name);
    }
}
