using System.Collections;
using DG.Tweening;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    [SerializeField] protected float Smooth;
    [SerializeField] protected bool CanMove = true;
    protected void Moveto(Vector3 position) {
        if(!CanMoveto(position)) return;
        this.CanMove =false;
        this.transform.parent.DOMove(position,Smooth).OnComplete(()=> {
            this.CanMove = true;
        });
    }
    protected void OnEnable() {
        InputManager.Instance.OnSwipeLeft += MovetoYZ;
        InputManager.Instance.OnSwipeRight += MovetoXY;
    }
    protected void OnDisable() {
        InputManager.Instance.OnSwipeLeft -= MovetoYZ;
        InputManager.Instance.OnSwipeRight -= MovetoXY;

    }
    protected IEnumerator BlockMove(float Time) {
        this.CanMove = false;
        yield return new WaitForSeconds(Time);
        this.CanMove = true;
    }
    protected bool CanMoveto(Vector3 position) {
        return CanMove;
    }
    protected void Facingto(Vector3 position) {
        this.transform.parent.forward = (position - this.transform.parent.position).normalized;
    }
    protected void Update() {
        this.Facingto(new Vector3(BoardManager.Instance.transform.position.x,this.transform.parent.position.y,BoardManager.Instance.transform.position.z));
    }
    protected void MovetoYZ() {
        if(BoardManager.Instance.boardMode == BoardManager.BoardMode.XY)      
        SoundSpawner.Instance.Spawn("tweet",this.transform.position,Quaternion.identity);
        this.Moveto(new Vector3(-10,10,4.5f)); 
        BoardManager.Instance.boardMode = BoardManager.BoardMode.ZY;
    }
    protected void MovetoXY() {
        if(BoardManager.Instance.boardMode == BoardManager.BoardMode.ZY)
        SoundSpawner.Instance.Spawn("tweet",this.transform.position,Quaternion.identity);
        this.Moveto(new Vector3(4.5f,10,-10));
        BoardManager.Instance.boardMode = BoardManager.BoardMode.XY;
    }
}
