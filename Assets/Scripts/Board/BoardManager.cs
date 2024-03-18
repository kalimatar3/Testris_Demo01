using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class BoardManager : MyBehaviour
{
    protected static BoardManager instance;
    public static BoardManager Instance { get => instance;}
    public BoardMode CurBoardMode;
    public bool CanChange;
    [SerializeField] protected BoardMode XY , YZ , YX , ZY;
    [SerializeField] protected Boardmatrix board {get; private set;}
    public Boardmatrix Board => board;
    public Transform SpawnPoint;
    public Transform CamPoint;
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null && instance.gameObject.activeInHierarchy) Destroy(this);
        else instance = this;
    }
    protected override void LoadComponents() {
        base.LoadComponents();
        this.LoadBoard();
    }
    protected virtual void OnEnable() {
        InputManager.Instance.OnSwipeLeft += XY.ChangetoRight;
        InputManager.Instance.OnSwipeLeft += ZY.ChangetoRight;
        InputManager.Instance.OnSwipeLeft += YZ.ChangetoRight;
        InputManager.Instance.OnSwipeLeft += YX.ChangetoRight;

        InputManager.Instance.OnSwipeRight += XY.ChangetoLeft;
        InputManager.Instance.OnSwipeRight += YX.ChangetoLeft;
        InputManager.Instance.OnSwipeRight += ZY.ChangetoLeft;
        InputManager.Instance.OnSwipeRight += YZ.ChangetoLeft;
    }
    protected void OnDisable() {
        InputManager.Instance.OnSwipeLeft -= XY.ChangetoRight;
        InputManager.Instance.OnSwipeLeft -= ZY.ChangetoRight;
        InputManager.Instance.OnSwipeLeft -= YZ.ChangetoRight;
        InputManager.Instance.OnSwipeLeft -= YX.ChangetoRight;

        InputManager.Instance.OnSwipeRight -= XY.ChangetoLeft;
        InputManager.Instance.OnSwipeRight -= YX.ChangetoLeft;
        InputManager.Instance.OnSwipeRight -= ZY.ChangetoLeft;
        InputManager.Instance.OnSwipeRight -= YZ.ChangetoLeft;
    }
    protected void LoadBoard() {
        this.board = GetComponentInChildren<Boardmatrix>();
        if(board == null) Debug.LogWarning(this.transform.name + " cant found BoardMatrix in Children");
    }
    protected override void Start() {
        base.Start();
       StartCoroutine(this.SetupDelay());
    }
    protected IEnumerator SetupDelay() {
        yield return new WaitUntil(predicate:()=> {
            if(BoardManager.instance == null) return false;
            return true;
        });
        XY.ChangetothisBoardMode();
    }
    protected virtual void FixedUpdate() {
        this.CanChange = CameraManager.Instance.CameraController.CanMove;
    }
}
