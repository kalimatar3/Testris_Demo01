using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xbutton : BaseButton
{
    protected override void Act() {
        base.Act();
        PanelManager.Instance.PanelController.DeActivePanel(transform.parent.name);
    } 
}
