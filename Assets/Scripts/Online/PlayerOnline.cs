using UnityEngine;
using Photon.Pun;

public class PlayerOnline : MonoBehaviour
{
    public bool win;
    public int Hp = 10;
    [PunRPC]
    public void SetWin() {
        this.win = true;
    }
    [PunRPC]
    public void ReduceHp() {
        this.Hp--;
    }
    [PunRPC]
    public void IcrHp() {
      this.Hp++;
    }
}
