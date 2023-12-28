using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MyBehaviour
{    
    protected static SoundManager instance;
    public static SoundManager Instance { get => instance ;}
    [SerializeField] protected AudioMixer ThisAudioMixer;
    [SerializeField] protected Slider MusicVolume,SoundEffectVolume;
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else  { 
            instance = this;
        }
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadVolume();
        this.LoadAudioMixer();
    }
    protected  void LoadAudioMixer()
    {
        if(ThisAudioMixer == null) Debug.LogWarning( this.transform + " Dont have AudioMixer");
    }
    protected override void Start()
    {
        base.Start();
        StartCoroutine(DelayStart());
    }
    protected IEnumerator DelayStart()
    {
        yield return new  WaitUntil( predicate: ()=>
        {
            if(DataManager.Instance == null) return false;
            return true;
        });
        this.MusicVolume.value = DataManager.Instance.DynamicData.MusicVolume;
        this.SoundEffectVolume.value = DataManager.Instance.DynamicData.SoundEffectVolume;
        this.SettingMusicVolume();
        this.SettingSoundEffectVolume();
    }
    protected void LoadVolume()
    {
        if(MusicVolume == null || SoundEffectVolume == null) Debug.LogWarning( this.transform + " Dont have Volume Slider");
    }
    public void SettingMusicVolume()
    {
        float Musicvolume = MusicVolume.value;
        ThisAudioMixer.SetFloat("Music",Mathf.Log10(Musicvolume) *20);
        DataManager.Instance.DynamicData.MusicVolume = Musicvolume;
        Lsmanager.Instance.SaveGame();
    }
    public void SettingSoundEffectVolume()
    {
        float SoundEffectvolume = SoundEffectVolume.value;
        ThisAudioMixer.SetFloat("Effect",Mathf.Log10(SoundEffectvolume) *20);   
        DataManager.Instance.DynamicData.SoundEffectVolume = SoundEffectvolume;
        Lsmanager.Instance.SaveGame();
    }
}
