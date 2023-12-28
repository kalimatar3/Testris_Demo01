using Photon.Pun;
using UnityEngine;
public class BoardMatrixOnline : MyBehaviour
{
    public TetrominoManagerInOnline TetrominoManagerInOnline;
    public BlockSpawnerInOnline BlockSpawnerInOnline;
    public BoardManagerOnline BoardManagerOnline;
    protected Transform[,,] Matrix = new Transform[10, 25, 10];
    protected bool IsRowFull(int row) {
        for(int i = 0; i < Matrix.GetLength(0); i++) {
            if(!IspipehasBlock(GetZArray(i,row)) || !IspipehasBlock(GetXArray(row,i))) {
                return false;
            }
        }
        return true;
    }
    public Transform[] GetXArray(int y,int z) {
        Transform[] Array = new Transform[Matrix.GetLength(0)];
        for(int i =0 ; i < Matrix.GetLength(0);i++) {
            Array[i] = Matrix[i,y,z];
        }
        return Array;
    }
    public Transform[] GetZArray(int x,int y) {
        Transform[] Array = new Transform[Matrix.GetLength(0)];
        for(int i = 0 ; i < Matrix.GetLength(2);i++) {
            Array[i] = Matrix[x,y,i];
        }
        return Array;
    }
    public Transform[,] GetXZ_2D_Array(int y) {
        Transform[,] Array_2D  = new Transform[Matrix.GetLength(0),Matrix.GetLength(2)];
        for(int i = 0 ; i < Matrix.GetLength(0); i++) {
            for(int j = 0 ; j < Matrix.GetLength(2); j++) {
                Array_2D[i,j] = Matrix[i,y,j];
            }
        }
        return Array_2D;
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
        PhotonView[] photonViews = PhotonNetwork.PhotonViews;
        foreach(PhotonView ele in photonViews) {
            if(!ele.IsMine && ele.GetComponent<PlayerOnline>() != null ) ele.RPC("ReduceHp",RpcTarget.All);
            if(ele.IsMine && ele.GetComponent<PlayerOnline>() != null) ele.RPC("IcrHp",RpcTarget.All);
        }
        BoardManagerOnline.SpawnPoint.localPosition = new Vector3(BoardManagerOnline.SpawnPoint.localPosition.x,OnlineGameMode.Instance.Hp,BoardManagerOnline.SpawnPoint.localPosition.z);
        SoundSpawner.Instance.Spawn("holdpower_cut",this.transform.position,Quaternion.identity);        
        Debug.Log("row : " + row + " is cleared");
        Transform[,,] newMatrix = new Transform[ Matrix.GetLength(0), Matrix.GetLength(1), Matrix.GetLength(2)];
        for(int i = 0; i < Matrix.GetLength(0); i++) {
            for(int k = 0; k < Matrix.GetLength(2); k++) {
                for(int j = 0 ; j < Matrix.GetLength(1); j++) {
                    if(j == row) {
                        if(Matrix[i,row,k] !=null && Matrix[i,row,k].gameObject.activeInHierarchy)
                        {
                            EffectSpawner.Instance.Spawn("Star",Matrix[i,row,k].position,Quaternion.identity);
                            BlockSpawnerInOnline.DeSpawnToPool(Matrix[i,row,k]);
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
    public void ClearAllMatrix() {
        foreach(Transform element in TetrominoManagerInOnline.TetrominoController.Cells) {
           BlockSpawnerInOnline.DeSpawnToPool(element);
        }
        for(int i = 0; i < Matrix.GetLength(0); i++) {
            for(int j = 0; j < Matrix.GetLength(1); j++) {
                for(int h = 0 ; h < Matrix.GetLength(2);h++) {
                if(Matrix[i,j,h] !=null && Matrix[i,j,h].gameObject.activeInHierarchy) BlockSpawnerInOnline.DeSpawnToPool(Matrix[i,j,h]);
                }
            }
        }
        this.Matrix = new Transform[Matrix.GetLength(0), Matrix.GetLength(1), Matrix.GetLength(2)];
    }
    public void AddtoMatrix(Transform transform) {
        if(!IsValidPosinMatrix(transform.localPosition)) return;
        this.Matrix[(int)transform.localPosition.x,(int)transform.localPosition.y,(int)transform.localPosition.z] = transform; 
    }
    public bool IsValidPosinMatrix(Vector3 position) {
        if(!OnMatrixsize(position)) return false;
        if(Matrix[(int)position.x,(int)position.y,(int)position.z] != null && Matrix[(int)position.x,(int)position.y,(int)position.z].gameObject.activeInHierarchy) return false;
        return true;
    }
    protected bool OnMatrixsize(Vector3 position) {
        if(position.x < 0 || position.x > 9) return false;
        if(position.y < 0 ) return false;
        if(position.z < 0 || position.z > 9) return false;
        return true;
    }
}
