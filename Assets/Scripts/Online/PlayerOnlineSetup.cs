using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnlineSetup : MonoBehaviour
{
    [SerializeField] protected List<Transform> ListLocalPlayerTranform;
    public void IslocalPlayer() {
        foreach(Transform element in ListLocalPlayerTranform) {
            element.gameObject.SetActive(true);
        }
    }
}
