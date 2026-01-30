using System;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using Debug = UnityEngine.Debug;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class LjudChef : MonoBehaviour
{
    public static LjudChef Instans { get; private set; }
    private Bank mästarbanken;
    
    [Header("Dessa variabler används för närvarande inte.")]
    public MaskLjud maskljud;
    public UILjud uILjud;
    public IntroLjud introLjud;
    public AffärsLjud affärsLjud;
    public FiskLjud fiskLjud;
    public FiskLjud maskeradFiskLjud;
    public MusikScript musikLjud;
    
    private void Awake()
    {
        if (Instans != null && Instans != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instans = this;
            transform.parent = null;
            DontDestroyOnLoad(this);
            RuntimeManager.StudioSystem.getBank("Master", out mästarbanken);
            mästarbanken.loadSampleData();
        }
    }

    public void SpelaEnskottsLjud(EventReference ljud)
    {
        RuntimeManager.PlayOneShot(ljud);
    }

    public EventInstance SpelaLjud(EventReference ljud)
    {
        #if UNITY_EDITOR
        Debug.Log($"Spelar event: {ljud.Path}");
        #endif
        
        EventInstance instans = RuntimeManager.CreateInstance(ljud);
        instans.start();
        return instans;
    }

    public void StoppaLjud(EventInstance ljud)
    {

        ljud.stop(STOP_MODE.ALLOWFADEOUT);
        ljud.release();
    }
}
