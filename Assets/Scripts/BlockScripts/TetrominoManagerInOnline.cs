using UnityEngine;

public class TetrominoManagerInOnline : MyBehaviour
{
    [SerializeField] protected SpawnTetrominInOnline spawnTetromino {get; private set;}
    public SpawnTetrominInOnline SpawnTetromino => spawnTetromino;
    [SerializeField] protected TetrominoControllerOnline tetrominoController {get; private set;}
    public TetrominoControllerOnline TetrominoController => tetrominoController;

    protected override void LoadComponents() {
        base.LoadComponents();
        this.LoadspawnTetromino();
        this.LoadTetrominoController();
    }
    protected void LoadspawnTetromino() {
        this.spawnTetromino = GetComponentInChildren<SpawnTetrominInOnline>();
        if(spawnTetromino == null) Debug.LogWarning(this.transform.name + " cant found spawnTetromino");
    }
    protected void LoadTetrominoController() {
        this.tetrominoController = GetComponentInChildren<TetrominoControllerOnline>();
        if(tetrominoController == null) Debug.LogWarning(this.transform.name + " cant found TetrominoController");
    }
}
