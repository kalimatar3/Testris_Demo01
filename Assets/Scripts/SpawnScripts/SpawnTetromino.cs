using UnityEngine;
public class SpawnTetromino : MyBehaviour
{
    public TetrominoManager TetrominoManager;
    public bool canSpawn;
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
        if(!this.canSpawn) return false;
        if(!TetrominoManager.TetrominoController.getLanded()) return false;
        return true;
    }
    protected void spawnTetromino() {
        if(!CanSapwn()) return;
        TetrominoManager.TetrominoController.Iniatialize(new Vector3(5,21,5));
    }
}
