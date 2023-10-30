using Unity.VisualScripting;
using UnityEngine;
public class InputManager : MyBehaviour
{
    protected static InputManager instance;
    public static InputManager Instance { get => instance ;}
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }
}
