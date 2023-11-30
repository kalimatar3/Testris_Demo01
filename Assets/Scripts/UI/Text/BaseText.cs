using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;
public class BaseTextUI : MyBehaviour
{
    protected Text text;
    protected override void LoadComponents() {
        base.LoadComponents();
        this.LoadText();
    }
    protected void LoadText() {
        this.text = GetComponent<Text>();
        if(text == null) Debug.LogWarning("Cant found Text");
    }
}
