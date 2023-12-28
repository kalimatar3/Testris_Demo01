using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MyBehaviour
{
    public const  int VerticalSize = 20;
    public const int HorizontalSize = 10;
    public enum BoardMode {
        XY,
        ZY,
    }
    protected static BoardManager instance;
    public static BoardManager Instance { get => instance;}
    [SerializeField] protected Boardmatrix board {get; private set;}
    public Boardmatrix Board => board;
    public BoardMode boardMode;
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
    protected void LoadBoard() {
        this.board = GetComponentInChildren<Boardmatrix>();
        if(board == null) Debug.LogWarning(this.transform.name + " cant found BoardMatrix in Children");
    }
}
