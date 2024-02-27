using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BoardmatrixOnline : Boardmatrix
{
    public override void ClearRow(int row)
    {
        if(!IsRowFull(row)) return;
        OnlineGameMode.Instance.Players[0].GetComponent<PhotonView>().RPC("IcrHp",RpcTarget.All);
        OnlineGameMode.Instance.Players[1].GetComponent<PhotonView>().RPC("ReduceHp",RpcTarget.All);
        SoundSpawner.Instance.Spawn("holdpower_cut",this.transform.position,Quaternion.identity);        
        Debug.Log("row : " + row + " is cleared");
        Transform[,,] newMatrix = new Transform[ Matrix.GetLength(0), Matrix.GetLength(1), Matrix.GetLength(2)];
        for(int i = 0; i < Matrix.GetLength(0); i++) {
            for(int k = 0; k < Matrix.GetLength(2); k++) {
                for(int j = 0 ; j < Matrix.GetLength(1); j++) {
                    if(j == row) {
                        if(Matrix[i,row,k] !=null && Matrix[i,row,k].gameObject.activeInHierarchy)
                        {
                            EffectSpawner.Instance.Spawn("Star",Matrix[i,row,k].localPosition,Quaternion.identity);
                            BlockSpawnerOnline.Instance.DeSpawnToPool(Matrix[i,row,k]);
                        } 
                    }
                    else if(j > row) {
                        if(Matrix[i,j,k] != null) {
                                Matrix[i,j,k].localPosition = new Vector3((int)Matrix[i,j,k].localPosition.x,(int)Matrix[i,j,k].localPosition.y-1,(int)Matrix[i,j,k].localPosition.z);
                                newMatrix[i,j-1,k] = Matrix[i,j,k];
                        }
                    }
                    else if(j < row) {
                        newMatrix[i,j,k] = Matrix[i,j,k];
                    }
                }
            }
        }
        this.Matrix = newMatrix;
    }
}
