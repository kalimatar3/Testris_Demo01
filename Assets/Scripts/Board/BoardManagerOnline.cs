using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManagerOnline : MyBehaviour
{
    public const  int VerticalSize = 20;
    public const int HorizontalSize = 10;
    public enum BoardMode {
        XY,
        ZY,
    }
    [SerializeField] protected BoardMatrixOnline board {get; private set;}
    public BoardMatrixOnline Board => board;
    public BoardMode boardMode;
    public Transform SpawnPoint;
    public Transform CamPoint;
    protected override void LoadComponents() {
        base.LoadComponents();
        this.LoadBoard();
    }
    protected void LoadBoard() {
        this.board = GetComponentInChildren<BoardMatrixOnline>();
        if(board == null) Debug.LogWarning(this.transform.name + " cant found BoardMatrix in Children");
    }
}
