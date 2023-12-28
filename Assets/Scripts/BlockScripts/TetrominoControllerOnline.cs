using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEditor;
using UnityEngine;
public class TetrominoControllerOnline : MyBehaviour
{
    public Vector3 test;
    public Transform[] Cells ;
    [SerializeField] protected Vector3Int[] CellsPosition;
    [SerializeField] protected List<TetrominoData> ListTettrominoData;
    public Vector3 Tetromino_Pos ;
    public TetrominoData ThisTetromino {get; private set;}
    [SerializeField] protected bool Landed;
    [SerializeField] protected float Speed;
    public BoardManagerOnline boardManagerOnline;
    public BlockSpawnerInOnline blockSpawnerInOnline;
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
    protected override void LoadComponents() {
        base.LoadComponents();
        this.LoadListTettrominoData();
    }
    protected void OnEnable() {
        InputManager.Instance.OnEndRight += MoveRight;
        InputManager.Instance.OnEndLeft += MoveLeft;
        InputManager.Instance.OnEndUnder += Rotate;
    }
    protected void OnDisable() {
        InputManager.Instance.OnEndRight -= MoveRight;
        InputManager.Instance.OnEndLeft -= MoveLeft;
        InputManager.Instance.OnEndUnder -= Rotate;
    }
    protected void LoadListTettrominoData() {
        if(ListTettrominoData.Count > 0) return;
        Tetromino[] arrayenum = (Tetromino[])Enum.GetValues(typeof(Tetromino));
        foreach(Tetromino element in arrayenum) {
            ListTettrominoData.Add(new TetrominoData(element));
        }
    }
    protected void MoveRight(Vector2 position,float time) 
    {
        SoundSpawner.Instance.Spawn("tuck",this.transform.position,Quaternion.identity);
        if(boardManagerOnline.boardMode == BoardManagerOnline.BoardMode.XY) {
            this.Move(Vector3.right);
        }
        else if(boardManagerOnline.boardMode == BoardManagerOnline.BoardMode.ZY) {
            if(PhotonNetwork.IsMasterClient) this.Move(Vector3.back);
            else this.Move(Vector3.forward);
        }
    }
    protected void MoveLeft(Vector2 position,float time) 
    {
        SoundSpawner.Instance.Spawn("tuck",this.transform.position,Quaternion.identity);
        if(boardManagerOnline.boardMode == BoardManagerOnline.BoardMode.XY)    this.Move(Vector3.left);
        else if(boardManagerOnline.boardMode == BoardManagerOnline.BoardMode.ZY)
        {
            if(PhotonNetwork.IsMasterClient) this.Move(Vector3.forward);
            else this.Move(Vector3.back);
        }  
    }
    protected void Rotate(Vector2 position,float time)  
    {
        SoundSpawner.Instance.Spawn("tuck",this.transform.position,Quaternion.identity);
        if(boardManagerOnline.boardMode == BoardManagerOnline.BoardMode.XY)    this.RotateZ(1);
        else if(boardManagerOnline.boardMode == BoardManagerOnline.BoardMode.ZY)    this.RotateX(1);
    }
    public void Main() {
        if(this.ThisTetromino == null) return;
        InputManager.Instance.touchcontrols.Enable();
        Timer += Time.deltaTime * 1f * Speed;
        if(Timer >= 1f) {
            Timer = 0;
            this.Move(Vector3.down);
        }
    }
    protected void RotateZ(int direction) {
        int maxX = 0;
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
            if(Tetromino_Pos.y + y <= 0) return;
            if(!OnBoardPosition(new Vector3Int(x,y,CellsPosition[i].z)+ Tetromino_Pos) ) {
                if(Tetromino_Pos.x + x < 0 && Tetromino_Pos.x + x - 0 < maxX )  maxX = x;
                else if(Tetromino_Pos.x + x > 9 && Tetromino_Pos.x + x - 9 > maxX)  maxX = x;
            }
            CellsPosition[i] =  new Vector3Int(x,y,CellsPosition[i].z);
        }
        this.Tetromino_Pos += new Vector3(1,0,0) * - maxX;
        this.MovingCell();
    }
    protected void RotateX(int direction) {
        int maxZ = 0;
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
            if(Tetromino_Pos.y + y <= 0) return;
            if(!OnBoardPosition(new Vector3Int(CellsPosition[i].x,y,z) + Tetromino_Pos)) {
                if(Tetromino_Pos.z + z < 0 && Tetromino_Pos.z + z - 0 < maxZ )  maxZ = z;
                else if(Tetromino_Pos.z + z > 9 && Tetromino_Pos.z + z - 9 > maxZ)  maxZ = z;
            }
            CellsPosition[i] =  new Vector3Int(CellsPosition[i].x,y,z);
        }
        this.Tetromino_Pos += new Vector3(0,0,1) * - maxZ;
        this.MovingCell();
    }
    // do after landed = true 
    protected void Landing() {
        SoundSpawner.Instance.Spawn("twuut",this.transform.position,Quaternion.identity);
        for(int i = 0; i < Cells.Length; i++) {
            boardManagerOnline.Board.AddtoMatrix(Cells[i]);
            Debug.Log(Cells[i].name + "add to Matrix" + " " + Cells[i].localPosition.x + " " + Cells[i].localPosition.y + " " + Cells[i].localPosition.z);
        }
        for(int i = 0; i < Cells.Length; i++) {
            boardManagerOnline.Board.ClearRow((int)Cells[i].localPosition.y);
        }
    }
    protected bool OnBoardPosition(Vector3 position) {
        if(position.x < 0 || position.x > 9) return false;
        if(position.y < 0) return false;
        if(position.z < 0 || position.z > 9) return false;
        return true;
    }
    protected bool IsValidPos(Vector3 position) {
        if(!OnBoardPosition(position)) return false;
        if(!boardManagerOnline.Board.IsValidPosinMatrix(position)) return false;  
        return true;         
    }
    protected bool CanMoveto(Vector3 position) {
        if(!IsValidPos(position)) return false;
        return true;
    }
    protected void MovingCell() {
        for(int i = 0 ; i < Cells.Length; i++) {
            Cells[i].localPosition = CellsPosition[i] + Tetromino_Pos;
        }
    }
    protected void Move(Vector3 translation) {
        foreach(Transform element in Cells) {
            if(!CanMoveto(element.localPosition + translation)) {
                    if(translation == Vector3.down) {           
                    this.setLanded(true);
                    this.Landing();
                    return;
                }
                else return;
                }
        }
        this.Tetromino_Pos += translation ;
        this.MovingCell();
    }
    public void Iniatialize(Transform transform) {
        String BlockTile = blockSpawnerInOnline.prefabs[UnityEngine.Random.Range(0,blockSpawnerInOnline.prefabs.Count )].transform.name;
        Tetromino_Pos = transform.localPosition;
        Vector3 SpawnPoint = transform.position;
        ThisTetromino = ListTettrominoData[UnityEngine.Random.Range(0,ListTettrominoData.Count)];
        CellsPosition = new Vector3Int[ThisTetromino.Cells.Length];
        Cells = new Transform[CellsPosition.Length];
        for(int i = 0 ; i < this.ThisTetromino.Cells.Length ; i++) {
            this.CellsPosition[i] = ThisTetromino.Cells[i];
            this.Cells[i] =  blockSpawnerInOnline.Spawn(BlockTile,this.CellsPosition[i] + SpawnPoint ,Quaternion.identity);
        }
        this.setLanded(false);
    }
}
