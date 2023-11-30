using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLessButton : BaseButton
{
    protected override void Act()
    {   
        GameModeManager.Instance.GameModeController.SetGameMode("Endless");
        PanelManager.Instance.PanelController.DeActivePanel("MainMenuPanel");
    }
}
