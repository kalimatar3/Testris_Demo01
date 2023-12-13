using UnityEngine;

public class ClassicModebutton : BaseButton
{
    protected override void Act()
    {
        base.Act();
        GameModeManager.Instance.GameModeController.SetGameMode("Classic");
        PanelManager.Instance.PanelController.DeActivePanel("MainMenuPanel");
    }
}
