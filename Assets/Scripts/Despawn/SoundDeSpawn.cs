using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDeSpawn : Despawnbytime
{
    protected override void DeSpawnObjects()
    {
        SoundSpawner.Instance.DeSpawnToPool(this.transform.parent);
    }
}
