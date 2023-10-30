using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MyBehaviour
{
    protected static GameManager instance;
    public static GameManager Instance { get => instance;}
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }
}
