using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
[DefaultExecutionOrder(-1)]
public class InputManager : MyBehaviour
{
    protected static InputManager instance;
    public static InputManager Instance { get => instance ;}
    public delegate void StartTouchLeftScreen(Vector2 position,float time);
    public event StartTouchLeftScreen OnStartLeft;
    public delegate void StartTouchRightScreen(Vector2 position,float time);
    public event StartTouchLeftScreen OnStartRight;
    public delegate void EndTouchLeftScreen(Vector2 position,float time);
    public event EndTouchLeftScreen OnEndLeft;
    public delegate void EndTouchRightScreen(Vector2 position,float time);
    public event EndTouchLeftScreen OnEndRight;
    public delegate void StartTouchUnderScreen(Vector2 position,float time);
    public event StartTouchUnderScreen OnStartUnder;
    public delegate void EndTouchUnderScreen(Vector2 position,float time);
    public event EndTouchUnderScreen OnEndUnder;


    public delegate void StartTouchEvent(Vector2 position,float time);
    public event StartTouchEvent OnStartTouch;
    public delegate void EndTouchEvent(Vector2 position,float time);
    public event EndTouchEvent OnEndTouch;
    public delegate void LeftSwipe();
    public event LeftSwipe OnSwipeLeft;
    public delegate void RightSwipe();
    public event RightSwipe OnSwipeRight;

    [SerializeField] private float minimumDistance = 15f;
    [SerializeField] private float maximumTime = 1f;
    [SerializeField, Range(0f, 1f)] private float directionThreshold = 0.9f;

    [SerializeField] private Vector2 startPosition, endPosition;
    private float startTime, endTime;

    public TouchControls touchcontrols;
    protected override void Awake()
    {
        base.Awake();
        this.touchcontrols = new TouchControls();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }
    protected void OnEnable() {
        OnStartTouch += SwipeStart;
        OnEndTouch += SwipeEnd;
        OnStartTouch += TouchStart;
        OnEndTouch += TouchEnd;
    }
    protected void OnDisable() 
    {
        OnStartTouch -= SwipeStart;
        OnEndTouch -= SwipeEnd;
        OnStartTouch -= TouchStart;
        OnEndTouch -= TouchEnd;

    }
    protected override void Start() {
        base.Start();
        this.touchcontrols.Touch.TouchPress.started += ctx => StartTouch(ctx);
        this.touchcontrols.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
    }
    protected void StartTouch(InputAction.CallbackContext context) {
        if(OnStartTouch == null) return;
        this.OnStartTouch(touchcontrols.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
    }
    protected void EndTouch(InputAction.CallbackContext context) {
        if(OnEndTouch == null) return;
        this.OnEndTouch(touchcontrols.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
    }
    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
        
    }
    private void TouchStart(Vector2 position,float time) {
        startPosition = position;
        startTime = time;
        if (touchcontrols.Touch.TouchPosition.ReadValue<Vector2>().x < Screen.width / 2 && touchcontrols.Touch.TouchPosition.ReadValue<Vector2>().y > Screen.height / 4)
        {
            if(OnStartLeft == null) return;
            this.OnStartLeft(position,time);
        }
        else if (touchcontrols.Touch.TouchPosition.ReadValue<Vector2>().x >= Screen.width / 2 && touchcontrols.Touch.TouchPosition.ReadValue<Vector2>().y > Screen.height / 4 )  
        {
            if(OnStartRight == null) return;
            this.OnStartRight(position,time);
        }
        else if(touchcontrols.Touch.TouchPosition.ReadValue<Vector2>().y <= Screen.height / 4 )
        {
            if(OnStartUnder == null) return;
            this.OnStartUnder(position,time);
        }

    }
    private void TouchEnd(Vector2 position,float time) {
        endPosition = position;
        endTime = time;
        if(Vector3.Distance(startPosition, endPosition) >= minimumDistance) return;
        if (touchcontrols.Touch.TouchPosition.ReadValue<Vector2>().x < Screen.width / 2 && touchcontrols.Touch.TouchPosition.ReadValue<Vector2>().y > Screen.height / 4)
        {
            if(OnEndLeft == null) return;
            this.OnEndLeft(position,time);
        }
        else if (touchcontrols.Touch.TouchPosition.ReadValue<Vector2>().x >= Screen.width / 2 && touchcontrols.Touch.TouchPosition.ReadValue<Vector2>().y > Screen.height / 4 )  
        {
            if(OnEndRight == null) return;
            this.OnEndRight(position,time);
        }
        else if(touchcontrols.Touch.TouchPosition.ReadValue<Vector2>().y <= Screen.height / 4 )
        {
            if(OnEndUnder == null) return;
            this.OnEndUnder(position,time);
        }

    }

    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if(Vector3.Distance(startPosition, endPosition) >= minimumDistance && (endTime - startTime) < maximumTime)
        {
            Vector3 direction = endPosition - startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2D);
        }
    }

    private void SwipeDirection(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            if (OnSwipeLeft != null) OnSwipeLeft();
        }
        else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            if (OnSwipeRight != null) OnSwipeRight();
        }
    }
}
