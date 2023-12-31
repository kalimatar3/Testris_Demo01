using System;
using UnityEngine;
[Serializable]
public enum Tetromino {
    I_I,L_I,L_L,Z_L,T_L // 3D block name... more
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