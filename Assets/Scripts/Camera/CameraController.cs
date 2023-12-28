using System.Collections;
using DG.Tweening;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    [SerializeField] protected float Smooth;
    [SerializeField] protected bool CanMove = true;
    protected void Start() {
        StartCoroutine(MovetoDelay());
    }
    protected IEnumerator MovetoDelay() {
        yield return new WaitUntil(predicate:() => {
            if(BoardManager.Instance == null) return false;
            return true;
        });
        this.Moveto(new Vector3(0,0,-10) + BoardManager.Instance.CamPoint.position);     
    }
    protected void Moveto(Vector3 position) {
        if(!CanMoveto(position)) return;
        this.CanMove =false;
        this.transform.parent.DOMove(position,Smooth).OnComplete(()=> {
            this.CanMove = true;
        });
    }
    protected void OnEnable() {
        InputManager.Instance.OnSwipeLeft +=MovetoXY;
        InputManager.Instance.OnSwipeRight += MovetoYZ;
    }
    protected void OnDisable() {
        InputManager.Instance.OnSwipeLeft -= MovetoXY;
        InputManager.Instance.OnSwipeRight -= MovetoYZ;

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
        if(BoardManager.Instance != null)
        this.Facingto(BoardManager.Instance.CamPoint.position);
    }
    protected void MovetoYZ() {
        if(BoardManager.Instance.boardMode == BoardManager.BoardMode.XY)      
        SoundSpawner.Instance.Spawn("tweet",this.transform.position,Quaternion.identity);
        this.Moveto(new Vector3(-10,0,0) + BoardManager.Instance.CamPoint.position); 
        BoardManager.Instance.boardMode = BoardManager.BoardMode.ZY;
    }
    protected void MovetoXY() {
        if(BoardManager.Instance.boardMode == BoardManager.BoardMode.ZY)
        SoundSpawner.Instance.Spawn("tweet",this.transform.position,Quaternion.identity);
        this.Moveto(new Vector3(0,0,-10) + BoardManager.Instance.CamPoint.position);
        BoardManager.Instance.boardMode = BoardManager.BoardMode.XY;
    }
}
