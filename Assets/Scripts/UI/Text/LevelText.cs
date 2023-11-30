using UnityEngine;

public class LevelText : BaseTextUI
{
    protected void FixedUpdate() {
        this.text.text = "Level : "  + (ClassicGameMode.Instance.CurLevel + 1).ToString();
    }
}
