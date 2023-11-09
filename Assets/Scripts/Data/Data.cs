using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Data
{
    public static Dictionary<Tetromino, Vector3Int[]> Cells = new Dictionary<Tetromino, Vector3Int[]>() 
    {
        {Tetromino.L_I, new Vector3Int[] { new Vector3Int(0,1,0), new Vector3Int(0,0,0), new Vector3Int(0,-1,0),new Vector3Int(-1,-1,0)} },
        {Tetromino.I_I, new Vector3Int[] {new Vector3Int(-1,0,0), new Vector3Int(0,0,0), new Vector3Int(1,0,0), new Vector3Int(2,0,0)} },
        //expend more....
    };
}
