using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MyBehaviour
{    
    protected static CameraManager instance;
    public static CameraManager Instance { get => instance;}
    protected CameraController cameraController;
    public CameraController CameraController => cameraController;
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null && instance.gameObject.activeInHierarchy) Destroy(Instance);
        else instance = this;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCameraController();
    }
    protected void LoadCameraController() {
        this.cameraController = GetComponentInChildren<CameraController>();
        if(cameraController == null) Debug.Log("Can found cameracontroller");
    }
}
