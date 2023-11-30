using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UIElements;
public class MovetoPos : MyBehaviour
{
    [SerializeField] protected Spawner spawner;
    [SerializeField] protected Transform Obj;
    [SerializeField] protected float TimetoMove;
    protected void OnEnable() {
    Vector3 Curpos = this.transform.position;
    this.transform.DOMove(RandomPosAround(Curpos),0.2f).OnComplete(()=> {        
        this.transform.DOMove(this.Obj.position + new Vector3(1,0,1) * Random.Range(-10,10)/10f,TimetoMove + Random.Range(0,20)/10f).OnComplete(()=> {
            GameMode.Instance.IcrPoint();
            spawner.DeSpawnToPool(this.transform);
            });
        });
    }
    protected Vector3 RandomPosAround(Vector3 position) {
        return position +  new Vector3(Random.Range(-20,21)/10f,Random.Range(0,21)/10f,Random.Range(-20,21)/10f);
    }
}
