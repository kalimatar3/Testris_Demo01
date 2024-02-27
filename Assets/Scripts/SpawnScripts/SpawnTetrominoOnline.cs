using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTetrominoOnline : SpawnTetromino
{
    public Transform SpawnPoint;
    public override void spawnTetromino() {
        if(!CanSapwn()) return;
        TetrominoManager.TetrominoController.Iniatialize(SpawnPoint);
    }
}
