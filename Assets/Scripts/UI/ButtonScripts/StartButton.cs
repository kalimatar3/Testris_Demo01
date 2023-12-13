using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : BaseButton
{
    [SerializeField] protected Transform DefaultButton;
    [SerializeField] protected Transform GameModebutton;
    protected override void Act()
    {
        base.Act();
        if(DefaultButton == null) return;
        if(GameModebutton == null) return;
        // animation...;
        this.DefaultButton.gameObject.SetActive(false);
        this.GameModebutton.gameObject.SetActive(true);
    }
}
