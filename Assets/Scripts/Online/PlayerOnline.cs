using UnityEngine;
using Photon.Pun;

public class PlayerOnline : MonoBehaviour
{
    public Transform test;
    [PunRPC]
    public void SetWin() {
        OnlineGameMode.Instance.win = true;
    }
    [PunRPC]
    public void ReduceHP() {
        OnlineGameMode.Instance.Hp --;
    }
    [PunRPC]
    public void IcrHp() {
        OnlineGameMode.Instance.Hp ++;
    }
}
