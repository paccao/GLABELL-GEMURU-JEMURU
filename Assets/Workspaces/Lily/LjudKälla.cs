using FMOD.Studio;
using FMODUnity;
using UnityEngine;

[CreateAssetMenu(fileName = "LjudRef", menuName = "Scriptable Objects/LjudRef")]
public class LjudKälla : ScriptableObject
{
    public EventReference ljudref, fotsteg, hopp, die, putt;
    public EventInstance ljudinstans;
    void OnEnable()
    {
        LjudChef.Instans.NyLjudKällaSkapades(this);
    }
}
