using UnityEngine;
using DG.Tweening;
using System.Collections;

public class ButtonMove : MyBehaviour
{
    [SerializeField] protected float Time;
    [SerializeField] protected RectTransform EndPos;
    [SerializeField] protected Vector3 Base;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Base = this.transform.position;
    }
    protected void OnEnable()
    {
        this.transform.position = this.Base;
        this.StartCoroutine(this.MoveDelay());
    }
    protected IEnumerator MoveDelay()
    {
        yield return new WaitForSeconds(0.6f);
        yield return new WaitUntil(predicate:()=>
        {
            if(EndPos == null) return false;
            return true;
        });
        this.transform.DOMove(this.EndPos.position,Time);
    }
}
