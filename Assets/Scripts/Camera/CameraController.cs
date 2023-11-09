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
        BoardManager.Instance.boardMode = (BoardManager.BoardMode)(((int)BoardManager.Instance.boardMode + 1) % 2);
        this.transform.parent.DOMove(position,Smooth).OnComplete(()=> {
            this.CanMove = true;
        });
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
        if(Input.GetKeyDown(KeyCode.Q)) {
            this.Moveto(new Vector3(this.transform.parent.position.z,this.transform.parent.position.y,this.transform.parent.position.x));
        }
    }
}
