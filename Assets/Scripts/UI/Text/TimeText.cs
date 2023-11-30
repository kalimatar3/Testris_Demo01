using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeText : BaseTextUI
{
    protected void FixedUpdate() {
        this.text.text = "Time : " + EndLessGameMode.Instance.GetTime().ToString("F2");
    }
}
