using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MyBehaviour
{    
    protected static CameraManager instance;
    public static CameraManager Instance { get => instance;}
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }
}
