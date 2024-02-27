using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoManagerOnline : TetrominoManager
{
    protected new static TetrominoManagerOnline instance;
    public new static TetrominoManagerOnline Instance { get => instance;}
    [SerializeField] protected SpawnTetrominoOnline spawnTetrominoOnline {get; private set;}
    public SpawnTetrominoOnline SpawnTetrominoOnline => this.spawnTetrominoOnline;
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null && instance.gameObject.activeInHierarchy) Destroy(this);
        else instance = this;
    }
    protected override void LoadspawnTetromino() {
        this.spawnTetrominoOnline = GetComponentInChildren<SpawnTetrominoOnline>();
        if(spawnTetrominoOnline == null) Debug.LogWarning(this.transform.name + " cant found spawnTetromino");
    }
}
