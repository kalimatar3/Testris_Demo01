using System.Collections;
using DG.Tweening;
using Photon.Realtime;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    [SerializeField] protected float Smooth;
    [SerializeField] public bool CanMove = true;
    [SerializeField] protected Transform ForcusPoint;
    public void Moveto(Vector3 position) {
        if(!CanMoveto(position)) return;
        this.CanMove =false;
        this.transform.parent.DOMove(position,Smooth).OnComplete(()=> {
            this.CanMove = true;
        });
    }
    protected bool CanMoveto(Vector3 position) {
        return CanMove;
    }
    protected void Facingto(Vector3 position) {
        Quaternion rotation = Quaternion.LookRotation((position - this.transform.parent.position).normalized,ForcusPoint.up);
        this.transform.parent.rotation = rotation;
    }
    protected void Update() {
        this.transform.parent.position = new Vector3(this.transform.parent.position.x,ForcusPoint.position.y,this.transform.parent.position.z);
        this.Facingto(ForcusPoint.position);
    }
}
