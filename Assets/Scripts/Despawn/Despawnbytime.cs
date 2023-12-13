using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawnbytime : Despawn
{
    protected float Timer;
    [SerializeField] public float DespawnTime;
    protected override bool CanDeSpawn()
    {
        Timer += Time.deltaTime * 1f;
        if(Timer > DespawnTime ) 
        {
            Timer = 0;
            return true;
        }
        else return false;
    }
}
