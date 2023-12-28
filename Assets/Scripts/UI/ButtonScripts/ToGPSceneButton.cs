using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToGPSceneButton : BaseButton
{
    protected override void Act() {
        base.Act();
        SceneManager.LoadScene("GamePlay");  
    }
}
