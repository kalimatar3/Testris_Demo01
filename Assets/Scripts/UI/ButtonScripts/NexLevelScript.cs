using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class NexLevelScript : BaseButton
{
    protected override void Act()
    {
        base.Act();
        ClassicGameMode.Instance.CurLevel ++;
        DataManager.Instance.DynamicData.Level = ClassicGameMode.Instance.CurLevel;
        Lsmanager.Instance.SaveGame();
        GameModeManager.Instance.GameModeController.SetGameMode("Classic");
        PanelManager.Instance.PanelController.DeActivePanel("ClassicMode:WinPanel");
    }
}
