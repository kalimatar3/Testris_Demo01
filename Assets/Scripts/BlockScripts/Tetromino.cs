using System;
using UnityEngine;
[Serializable]
public enum Tetromino {
    I_I // 3D block name... more
}
[Serializable]
public class TetrominoData  {
    public Tetromino Tetromino;
    public  Vector3Int[] Cells ;
    public TetrominoData(Tetromino tetromino) {
        this.Tetromino = tetromino;
        this.Cells = Data.Cells[tetromino];
    }
}