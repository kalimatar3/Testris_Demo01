using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class BlockSpawnerInOnline : Spawner
{
    public override Transform GetObjectFromPool(Transform Prefab, Vector3 position, Quaternion rotation)
    {
        foreach(Transform obj in PoolObjs)
        {
            if(Prefab.name == obj.name)
            {
                PoolObjs.Remove(obj);
                obj.transform.position = position;
                obj.transform.rotation = rotation;
                return obj;
            }
        }
        Transform NewPrefab = PhotonNetwork.Instantiate(Prefab.name, position, rotation).transform;
        NewPrefab.name = Prefab.name;
        return NewPrefab;
    }

}
