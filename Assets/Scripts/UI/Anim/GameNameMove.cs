using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  DG.Tweening;

public class GameNameMove : MonoBehaviour
{
    [SerializeField] protected float Time;
    protected void OnEnable()
    {
        this.StartCoroutine(this.Appear());
    }
    protected IEnumerator Appear()
    {
        this.transform.localScale = new Vector3(this.transform.localScale.x,0,this.transform.localScale.z);
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(predicate:()=>
        {
            if(this.transform.localScale.y > 0) return false;
            return true;
        });
        this.transform.DOScaleY(1,Time);
    }
    protected void OnDisable()
    {
        this.transform.localScale = new Vector3(1,0,1);
    }    
}
