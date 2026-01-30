using System;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMOD.Studio;
using FMODUnity;

public class LjudChef : MonoBehaviour
{
    public static LjudChef Instans { get; private set; }
    private Bank mästarbanken;
    private List<LjudKälla> ljudreferenser;
    public LjudKälla sounds;

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
    
    public List<LjudKälla> GetEventInstances()
    {
        return ljudreferenser;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RuntimeManager.PlayOneShot(sounds.fotsteg);
    }

    public void NyLjudKällaSkapades(LjudKälla ljudref)
    {
        
    }
}
