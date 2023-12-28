using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameMode : MyBehaviour
{
    protected static GameMode instance;
    public static GameMode Instance { get => instance;}
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) this.gameObject.SetActive(false);
        else instance = this;
    }
    [SerializeField] protected float Point = 0;
    [SerializeField] protected float PointcanGet;
    public virtual void IcrPoint() {
        Point += PointcanGet;
    }
    public virtual void setPoint(float number) {
        Point = number;
    }
    public float getPoint() {
        return Point;
    }
    public abstract bool CanWin();
    public abstract void Win();
    public abstract bool CanLose();
    public abstract void Lose();
    public abstract bool CanStart();
    public abstract void Init();
    public abstract void Playing();
    public virtual IEnumerator Play() {
        yield return new WaitUntil(predicate:() => {
            return this.CanStart();
        });
        this.Init();
        while(true) {
            this.Playing();
            if(CanLose()) {
                this.Lose();
                BoardManager.Instance.Board.ClearAllMatrix();
                break;
            }
            if(CanWin()) {
                this.Win();
                BoardManager.Instance.Board.ClearAllMatrix();
                break;
            }
            yield return new WaitForSecondsRealtime(0f);
        }
    }
}
