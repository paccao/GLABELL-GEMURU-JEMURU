using FMOD.Studio;
using FMODUnity;
using UnityEngine;

[CreateAssetMenu(fileName = "AmbiansLjud", menuName = "Scriptable Objects/AmbiansLjud")]
public class AmbiansLjud : ScriptableObject
{
    public EventReference ovanförVatten, underVatten;
    private EventInstance ovanförInstans, underInstans;
        

    public void StartaOvanförVatten()
    {
        ovanförInstans = LjudChef.Instans.SpelaLjud(ovanförVatten);
    }

    public void StartaUnderVatten()
    {
        underInstans = LjudChef.Instans.SpelaLjud(underVatten);
    }

    public void StoppaOvanförVatten()
    {
        LjudChef.Instans.StoppaLjud(ovanförInstans);
    }

    public void StoppaUnderVatten()
    {
        LjudChef.Instans.StoppaLjud(underInstans);
    }
}
