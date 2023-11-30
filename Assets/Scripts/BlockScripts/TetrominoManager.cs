using UnityEngine;

public class TetrominoManager : MyBehaviour
{
    protected static TetrominoManager instance;
    public static TetrominoManager Instance { get => instance;}
    [SerializeField] protected SpawnTetromino spawnTetromino {get; private set;}
    public SpawnTetromino SpawnTetromino => spawnTetromino;
    [SerializeField] protected TetrominoController tetrominoController {get; private set;}
    public TetrominoController TetrominoController => tetrominoController;

    protected override void LoadComponents() {
        base.LoadComponents();
        this.LoadspawnTetromino();
        this.LoadTetrominoController();
    }
    protected void LoadspawnTetromino() {
        this.spawnTetromino = GetComponentInChildren<SpawnTetromino>();
        if(spawnTetromino == null) Debug.LogWarning(this.transform.name + " cant found spawnTetromino");
    }
    protected void LoadTetrominoController() {
        this.tetrominoController = GetComponentInChildren<TetrominoController>();
        if(tetrominoController == null) Debug.LogWarning(this.transform.name + " cant found TetrominoController");
    }
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }

}
