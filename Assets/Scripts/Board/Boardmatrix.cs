using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Boardmatrix : MyBehaviour
{
    protected Transform[,,] Matrix = new Transform[10, 20, 10];
    protected bool IsRowFull(int row) {
        for(int i = 0; i < Matrix.GetLength(0); i++) {
            if(!IspipehasBlock(ConvertMatrixtoZArray(i,row)) || !IspipehasBlock(ConvertMatrixtoXArray(row,i))) {
                return false;
            }
        }
        return true;
    }
    protected Transform[] ConvertMatrixtoXArray(int y,int z) {
        Transform[] Array = new Transform[Matrix.GetLength(0)];
        for(int i =0 ; i < Matrix.GetLength(0);i++) {
            Array[i] = Matrix[i,y,z];
        }
        return Array;
    }
    protected Transform[] ConvertMatrixtoZArray(int x,int y) {
        Transform[] Array = new Transform[Matrix.GetLength(0)];
        for(int i =0 ; i < Matrix.GetLength(2);i++) {
            Array[i] = Matrix[x,y,i];
        }
        return Array;
    }
    // return true if pipe has at least 1 block,return false if not 
    protected bool IspipehasBlock(Transform[] pipe) {
        foreach(Transform element in pipe) {
            if(element != null && element.gameObject.activeInHierarchy) return true;
        }
        return false;
    }
    public void ClearRow(int row) {
        if(!IsRowFull(row)) return;
        for(int i = 0; i < Matrix.GetLength(0); i++) {
            for(int j = 0; j < Matrix.GetLength(2); j++) {
                if(Matrix[i,row,j] !=null && Matrix[i,row,j].gameObject.activeInHierarchy) BlockSpawner.Instance.DeSpawnToPool(Matrix[i,row,j]);
            }
        }
        Debug.Log("row : " + row + " is cleared");
        Transform[,,] newMatrix = new Transform[ Matrix.GetLength(0), Matrix.GetLength(1), Matrix.GetLength(2)];
        for(int j = row + 1 ; j < Matrix.GetLength(1); j++) {
            for(int i = 0; i < Matrix.GetLength(0); i++) {
                for(int k = 0; k < Matrix.GetLength(2); k++) {
                   if(Matrix[i,j,k] != null) {
                        Matrix[i,j,k].position = new Vector3((int)Matrix[i,j,k].position.x,(int)Matrix[i,j,k].position.y-1,(int)Matrix[i,j,k].position.z);
                        newMatrix[i,j-1,k] = Matrix[i,j,k];
                   }
                }
            }
        }
        this.Matrix = newMatrix;
    }
    public void AddtoMatrix(Transform transform) {
        this.Matrix[(int)transform.position.x,(int)transform.position.y,(int)transform.position.z] = transform; 
    }
    public bool IsValidPosinMatrix(Vector3 position) {
        if(Matrix[(int)position.x,(int)position.y,(int)position.z] != null && Matrix[(int)position.x,(int)position.y,(int)position.z].gameObject.activeInHierarchy) return false;
        return true;
    }
}
