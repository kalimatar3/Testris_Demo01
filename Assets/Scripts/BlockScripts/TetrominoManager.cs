using UnityEngine;

public class TetrominoManager : MyBehaviour
{
    protected static TetrominoManager instance;
    public static TetrominoManager Instance { get => instance;}
    [SerializeField] private SpawnTetromino spawnTetromino;
    public SpawnTetromino SpawnTetromino => spawnTetromino;
    [SerializeField] private TetrominoController tetrominoController;
    public TetrominoController TetrominoController => tetrominoController;

    protected override void LoadComponents() {
        base.LoadComponents();
        this.LoadspawnTetromino();
        this.LoadTetrominoController();
    }
    protected virtual void LoadspawnTetromino() {
        this.spawnTetromino = GetComponentInChildren<SpawnTetromino>();
        if(spawnTetromino == null) Debug.LogWarning(this.transform.name + " cant found spawnTetromino");
    }
    protected virtual void LoadTetrominoController() {
        this.tetrominoController = GetComponentInChildren<TetrominoController>();
        if(tetrominoController == null) Debug.LogWarning(this.transform.name + " cant found TetrominoController");
    }
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null && instance.gameObject.activeInHierarchy) Destroy(this);
        else instance = this;
    }

}
