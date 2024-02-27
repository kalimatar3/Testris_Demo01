using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class TetrominoControllerOnline : TetrominoController
{
    protected override void MoveLeft(Vector2 position, float time)
    {
        SoundSpawner.Instance.Spawn("tuck",this.transform.position,Quaternion.identity);
        if(BoardManagerOnline.Instance.CurBoardMode.Name == "XY")    {
            if(PhotonNetwork.IsMasterClient) this.Move(Vector3.left);
            else this.Move(Vector3.right);
        }
        else if(BoardManagerOnline.Instance.CurBoardMode.Name == "YX")   {
            if(PhotonNetwork.IsMasterClient) this.Move(Vector3.right);
            else this.Move(Vector3.left);
        }
        else if(BoardManagerOnline.Instance.CurBoardMode.Name == "ZY")   this.Move(Vector3.forward);
        else if(BoardManagerOnline.Instance.CurBoardMode.Name == "YZ")   this.Move(Vector3.back);
    }
    protected override void MoveRight(Vector2 position, float time)
    {
        SoundSpawner.Instance.Spawn("tuck",this.transform.position,Quaternion.identity);
        if(BoardManagerOnline.Instance.CurBoardMode.Name == "XY") {
            if(PhotonNetwork.IsMasterClient)  this.Move(Vector3.right);
            else this.Move(Vector3.left);
        }
        else if(BoardManagerOnline.Instance.CurBoardMode.Name == "YX") {
            if(PhotonNetwork.IsMasterClient) this.Move(Vector3.left);
            else this.Move(Vector3.right);
        }
        else if(BoardManagerOnline.Instance.CurBoardMode.Name == "ZY") this.Move(Vector3.back);
        else if(BoardManagerOnline.Instance.CurBoardMode.Name == "YZ") this.Move(Vector3.forward);
    }
}
