using UnityEditor;
using UnityEngine;
public class DataManager : MyBehaviour
{
    protected static DataManager instance;
    public static DataManager Instance { get => instance;}
    public DynamicData DynamicData;
    protected override void Awake()
    {
        base.Awake();
        if(instance != null && instance != this)
        {
            Destroy(this);
            Debug.LogWarning(this.gameObject + "Does Existed");
        }
        else 
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }
    public virtual void LoadDataFromJson(string JsonString)
    {
        DynamicData Data = JsonUtility.FromJson<DynamicData>(JsonString);
        if(System.Runtime.InteropServices.Marshal.SizeOf(Data) == 0) return;
        Debug.Log(Data.Level);
        this.DynamicData = Data;
    }
    public virtual void ClearJson()
    {
    }
}
