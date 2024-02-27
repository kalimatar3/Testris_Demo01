using Unity.VisualScripting;
using UnityEngine;
public class SpawnTetromino : MyBehaviour
{
    public TetrominoManager TetrominoManager;
    protected override void LoadComponents() {
        base.LoadComponents();
        this.LoadTetrominoManager();
    }
    protected void LoadTetrominoManager() {
        this.TetrominoManager = GetComponentInParent<TetrominoManager>();
        if(TetrominoManager == null) Debug.Log(this.transform.name + "Cant found TetrominoManager"); 
    }
    protected bool CanSapwn() {
        if(!TetrominoManager.TetrominoController.getLanded()) return false;
        return true;
    }
    public virtual void spawnTetromino() {
        if(!CanSapwn()) return;
        TetrominoManager.TetrominoController.Iniatialize(BoardManager.Instance.SpawnPoint);
    }
}
