using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TetrominoController : MyBehaviour
{
    [HideInInspector] public Transform[] Cells { get; private set;}
    protected Vector3Int[] CellsPosition;
    [SerializeField] protected List<TetrominoData> ListTettrominoData;
    public Vector3 Tetromino_Pos {get; private set;}
    public TetrominoData ThisTetromino {get; private set;}
    [SerializeField] protected bool Landed;
    [SerializeField] protected float Speed;
    protected float Timer;
    public void setLanded(bool Landed) {
        this.Landed = Landed;
    }
    public bool getLanded() {
        return this.Landed;
    }
    public void setSpeed(float Speed) {
        this.Speed = Speed;
    }
    public float getSpeed() {
        return this.Speed;
    }
    protected override void LoadComponents() {
        base.LoadComponents();
        this.LoadListTettrominoData();
    }
    protected void LoadListTettrominoData() {
        if(ListTettrominoData.Count > 0) return;
        Tetromino[] arrayenum = (Tetromino[])Enum.GetValues(typeof(Tetromino));
        foreach(Tetromino element in arrayenum) {
            ListTettrominoData.Add(new TetrominoData(element));
        }
    }
    protected void Update() {
        if(this.ThisTetromino == null) return;
        if(BoardManager.Instance.boardMode == BoardManager.BoardMode.XY) {
            if(Input.GetKeyDown(KeyCode.Space)) this.RotateZ(1);
            if(Input.GetKeyDown(KeyCode.A)) this.Move(Vector3.left);
            else if(Input.GetKeyDown(KeyCode.D)) this.Move(Vector3.right);
        }
        if(BoardManager.Instance.boardMode == BoardManager.BoardMode.ZY) {
            if(Input.GetKeyDown(KeyCode.Space)) this.RotateX(1);
            if(Input.GetKeyDown(KeyCode.A)) this.Move(Vector3.forward);
            else if(Input.GetKeyDown(KeyCode.D)) this.Move(Vector3.back);
        }

        Timer += Time.deltaTime * 1f * Speed;
        if(Timer >= 1f) {
            Timer = 0;
            this.Move(Vector3.down);
        }
    }
    protected void RotateZ(int direction) {
        for(int i = 0 ; i < CellsPosition.Length;i++) {
            Vector3 cell = CellsPosition[i];
            int x,y;
            switch (this.ThisTetromino.Tetromino) 
            {
                case Tetromino.I_I:
                    cell.x -= 0.5f;
                    cell.y -=0.5f;
                    x = Mathf.CeilToInt((cell.x * Mathf.Cos(Mathf.PI / 2f) * direction) + ( cell.y * Mathf.Sin(Mathf.PI / 2f) * direction));
                    y = Mathf.CeilToInt((cell.x * -1f * Mathf.Sin(Mathf.PI / 2f) * direction) + (cell.y * Mathf.Cos(Mathf.PI / 2f) * direction));
                    break;
                default:
                    x = Mathf.RoundToInt((cell.x * Mathf.Cos(Mathf.PI / 2f) * direction) + ( cell.y * Mathf.Sin(Mathf.PI / 2f) * direction));
                    y = Mathf.RoundToInt((cell.x * -1f * Mathf.Sin(Mathf.PI / 2f) * direction) + (cell.y * Mathf.Cos(Mathf.PI / 2f) * direction));
                    break;
            }
            CellsPosition[i] =  new Vector3Int(x,y,CellsPosition[i].z);
        }
        this.MovingCell();
    }
    protected void RotateX(int direction) {
        for(int i = 0 ; i < CellsPosition.Length;i++) {
            Vector3 cell = CellsPosition[i];
            int z,y;
            switch (this.ThisTetromino.Tetromino) 
            {
                case Tetromino.I_I:
                    cell.z -= 0.5f;
                    cell.y -=0.5f;
                    z = Mathf.CeilToInt((cell.z * Mathf.Cos(Mathf.PI / 2f) * direction) + ( cell.y * Mathf.Sin(Mathf.PI / 2f) * direction));
                    y = Mathf.CeilToInt((cell.z * -1f * Mathf.Sin(Mathf.PI / 2f) * direction) + (cell.y * Mathf.Cos(Mathf.PI / 2f) * direction));
                    break;
                default:
                    z = Mathf.RoundToInt((cell.z * Mathf.Cos(Mathf.PI / 2f) * direction) + ( cell.y * Mathf.Sin(Mathf.PI / 2f) * direction));
                    y = Mathf.RoundToInt((cell.z * -1f * Mathf.Sin(Mathf.PI / 2f) * direction) + (cell.y * Mathf.Cos(Mathf.PI / 2f) * direction));
                    break;
            }
            CellsPosition[i] =  new Vector3Int(CellsPosition[i].x,y,z);
        }
        this.MovingCell();
    }

    // do after landed = true 
    protected void Landing() {
        for(int i = 0; i < Cells.Length; i++) {
            BoardManager.Instance.Board.AddtoMatrix(Cells[i]);
            Debug.Log(Cells[i].name + "add to Matrix" + " " + Cells[i].position.x + " " + Cells[i].position.y + " " + Cells[i].position.z);
        }
        for(int i = 0; i < Cells.Length; i++) {
            BoardManager.Instance.Board.ClearRow((int)Cells[i].position.y);
            Debug.Log("row : " + (int)Cells[i].position.y + " is cleared");
        }
    }
    protected bool OnBoardPosition(Vector3 position) {
        if(position.x < -BoardManager.HorizontalSize/2 + BoardManager.Instance.transform.position.x || position.x > BoardManager.HorizontalSize/2 + BoardManager.Instance.transform.position.x -1) return false;
        if(position.y < -BoardManager.VerticalSize/2 + BoardManager.Instance.transform.position.y) return false;
        if(position.z < -BoardManager.HorizontalSize/2 + BoardManager.Instance.transform.position.z || position.z > BoardManager.HorizontalSize/2 + BoardManager.Instance.transform.position.z- 1) return false;
        return true;
    }
    protected bool IsValidPos(Vector3 position) {
        if(!OnBoardPosition(position)) return false;
        if(!BoardManager.Instance.Board.IsValidPosinMatrix(position)) return false;  
        return true;         
    }
    protected bool CanMoveto(Vector3 position) {
        foreach(Transform element in Cells) {
            if(!IsValidPos(element.position + position)) {
                if(position == Vector3.down) {
                    this.setLanded(true);
                    this.Landing();
                }
                return false;
           }
        }
        return true;
    }
    protected void MovingCell() {
        for(int i = 0 ; i < Cells.Length; i++) {
            Cells[i].position = CellsPosition[i] + Tetromino_Pos;
        }
    }
    protected void Move(Vector3 translation) {
        if(!CanMoveto(translation)) return;
        this.Tetromino_Pos += translation;
        this.MovingCell();
    }
    public void Iniatialize(Vector3 position) {
        String BlockTile = BlockSpawner.Instance.prefabs[UnityEngine.Random.Range(0,BlockSpawner.Instance.prefabs.Count )].transform.name;
        Tetromino_Pos = position;
        ThisTetromino = ListTettrominoData[UnityEngine.Random.Range(0,ListTettrominoData.Count)];
        CellsPosition = new Vector3Int[ThisTetromino.Cells.Length];
        Cells = new Transform[CellsPosition.Length];
        for(int i = 0 ; i < this.ThisTetromino.Cells.Length ; i++) {
            this.CellsPosition[i] = ThisTetromino.Cells[i];
            this.Cells[i] =  BlockSpawner.Instance.Spawn(BlockTile,this.CellsPosition[i] + Tetromino_Pos ,Quaternion.identity);
        }
        this.setLanded(false);
    }
}
