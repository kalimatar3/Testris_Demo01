using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AppearPanel : MyBehaviour
{
    protected void OnEnable() {
        StartCoroutine(this.Appearing());
    }
    protected IEnumerator Appearing() {
        this.transform.localScale = new Vector3(1,0,1);
        yield return new WaitUntil(predicate: ()=> {
            return this.transform.localScale.y == 0;
        });
        this.transform.DOScale(new Vector3(1,1,1),0.1f)
        .SetEase(Ease.Linear);
        yield return new WaitForSeconds(0.8f);
        yield return new WaitUntil(predicate: ()=> {
            return this.transform.localScale.y == 1;
        });
        this.transform.DOScale(new Vector3(1,0,1),0.1f)
        .SetEase(Ease.Linear);
        yield return new WaitUntil(predicate: ()=> {
            return this.transform.localScale.y == 0;
        });
        this.transform.gameObject.SetActive(false);
    }
}
