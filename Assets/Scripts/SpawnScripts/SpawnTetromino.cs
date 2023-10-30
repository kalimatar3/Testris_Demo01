using UnityEngine;
public class SpawnTetromino : MyBehaviour
{
    public TetrominoManager TetrominoManager;
    protected override void LoadComponents() {
        base.LoadComponents();
        this.LoadTetrominoManager();
    }
    protected void FixedUpdate() {
        this.spawnTetromino();
    }
    protected void LoadTetrominoManager() {
        this.TetrominoManager = GetComponentInParent<TetrominoManager>();
        if(TetrominoManager == null) Debug.Log(this.transform.name + "Cant found TetrominoManager"); 
    }
    protected bool CanSapwn() {
        if(!TetrominoManager.TetrominoController.getLanded()) return false;
        return true;
    }
    protected void spawnTetromino() {
        if(!CanSapwn()) return;
        TetrominoManager.TetrominoController.Iniatialize(new Vector3(BoardManager.Instance.transform.position.x,BoardManager.Instance.transform.position.y + BoardManager.VerticalSize/2 - 4,BoardManager.Instance.transform.position.z));
    }
}
