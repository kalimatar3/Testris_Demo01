using System.Collections;
using DG.Tweening;
using UnityEngine;
public class CameraControllerOnline : MonoBehaviour
{
    public BoardManagerOnline boardManagerOnline;
    [SerializeField] protected float Smooth;
    [SerializeField] protected bool CanMove = true;
    protected void Start() {
        StartCoroutine(MovetoDelay());
    }
    protected IEnumerator MovetoDelay() {
        yield return new WaitUntil(predicate:() => {
            if(boardManagerOnline == null) return false;
            return true;
        });
        this.MovetoXY();
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
        if(boardManagerOnline != null)
        this.Facingto(boardManagerOnline.CamPoint.position);
        this.transform.parent.position = new Vector3(this.transform.parent.position.x,boardManagerOnline.CamPoint.position.y,this.transform.parent.position.z);
    }
    protected void MovetoYZ() {
        if(boardManagerOnline.boardMode == BoardManagerOnline.BoardMode.XY)      
        SoundSpawner.Instance.Spawn("tweet",this.transform.position,Quaternion.identity);
        this.Moveto(new Vector3(-10,0,0) + boardManagerOnline.CamPoint.position); 
        boardManagerOnline.boardMode = BoardManagerOnline.BoardMode.ZY;
    }
    protected void MovetoXY() {
        if(boardManagerOnline.boardMode == BoardManagerOnline.BoardMode.ZY)
        SoundSpawner.Instance.Spawn("tweet",this.transform.position,Quaternion.identity);
        this.Moveto(new Vector3(0,0,-10) + boardManagerOnline.CamPoint.position);
        boardManagerOnline.boardMode = BoardManagerOnline.BoardMode.XY;
    }
}
