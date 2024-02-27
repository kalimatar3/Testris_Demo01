using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class BlockSpawnerOnline : BlockSpawner
{
    protected new static BlockSpawnerOnline instance;
    public new static BlockSpawnerOnline Instance { get => instance ;}
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }
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
        NewPrefab.SetParent(base.Holder);  
        NewPrefab.name = Prefab.name;
        return NewPrefab;
    }
}
