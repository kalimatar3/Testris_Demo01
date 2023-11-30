using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PointText : BaseTextUI
{
    protected void FixedUpdate() {
        this.text.text = "Point :" + GameMode.Instance.getPoint().ToString();
    }
}
