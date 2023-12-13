using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Despawn : MyBehaviour
{
    protected virtual void FixedUpdate()
    {
        this.DeSpawning();
    }
    protected virtual void DeSpawning()
    {
        if(this.CanDeSpawn())
        {
            this.DeSpawnObjects();
        }
    }
    protected abstract bool CanDeSpawn();
    protected virtual void DeSpawnObjects()
    {
       //
    }
}
