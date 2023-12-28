using Unity.VisualScripting;
using UnityEngine;
public class SpawnTetrominInOnline : MyBehaviour
{
    public TetrominoManagerInOnline TetrominoManager;
    public BoardManagerOnline boardManagerOnline;
    protected override void LoadComponents() {
        base.LoadComponents();
        this.LoadTetrominoManager();
    }
    protected void LoadTetrominoManager() {
        this.TetrominoManager = GetComponentInParent<TetrominoManagerInOnline>();
        if(TetrominoManager == null) Debug.Log(this.transform.name + "Cant found TetrominoManager"); 
    }
    protected bool CanSapwn() {
        if(!TetrominoManager.TetrominoController.getLanded()) return false;
        return true;
    }
    public void spawnTetromino() {
        if(!CanSapwn()) return;
        TetrominoManager.TetrominoController.Iniatialize(boardManagerOnline.SpawnPoint);
    }
}
