using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMode : MyBehaviour
{
    public string Name;
    public Vector3 CamPos;
    public BoardMode Left,Right;
    protected void MoveCam() {
        SoundSpawner.Instance.Spawn("tweet",this.transform.position,Quaternion.identity);
        CameraManager.Instance.CameraController.Moveto(this.CamPos + transform.GetComponentInParent<BoardManager>().CamPoint.position);
    }
    public void ChangetothisBoardMode() {
        if(this == transform.GetComponentInParent<BoardManager>().CurBoardMode) return;  
        if(!transform.GetComponentInParent<BoardManager>().CanChange) return;
        StartCoroutine(this.ChangetothisBoardModeDelay());
        this.MoveCam();
    }
    protected IEnumerator ChangetothisBoardModeDelay() {
        yield return new WaitUntil(predicate:() => {
            if(transform.GetComponentInParent<BoardManager>() == null) return false;
            if(!transform.GetComponentInParent<BoardManager>().CanChange) return false;
            return true;
        });
        transform.GetComponentInParent<BoardManager>().CurBoardMode = this;
    }
    public void ChangetoLeft() { 
        if(this != transform.GetComponentInParent<BoardManager>().CurBoardMode) return;
        if(Left == null) return;      
        Left.ChangetothisBoardMode();
    }
    public void ChangetoRight() {
        if(this != transform.GetComponentInParent<BoardManager>().CurBoardMode) return;
        if(Right == null) return;
        Right.ChangetothisBoardMode();  
    }
}
