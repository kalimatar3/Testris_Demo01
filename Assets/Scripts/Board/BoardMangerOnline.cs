using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BoardManagerOnline : BoardManager
{
    protected new static BoardManagerOnline instance;
    public new static BoardManagerOnline Instance { get => instance;}
    [SerializeField] public Transform Model;

    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null && instance.gameObject.activeInHierarchy) Destroy(this);
        else instance = this;
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(OnlineGameMode.Instance == null) return;
        if(OnlineGameMode.Instance.Players[0] == null) return;
        CamPoint.localPosition = new Vector3(CamPoint.localPosition.x,OnlineGameMode.Instance.Players[0].GetComponent<PlayerOnline>().Hp - 10,CamPoint.localPosition.z);
    }
}
