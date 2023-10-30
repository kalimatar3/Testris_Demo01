using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BlockSpawner : Spawner
{
    protected static BlockSpawner instance;
    public static BlockSpawner Instance { get => instance ;}
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }
}
