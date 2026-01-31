using FMOD.Studio;
using FMODUnity;
using UnityEngine;

[CreateAssetMenu(fileName = "MusikLjud", menuName = "Scriptable Objects/MusikLjud")]
public class MusikScript : ScriptableObject
{
    public EventReference stridsmusik, seger, förlust, affärsmusik, menymusik;
    private EventInstance stridsinstans, segerinstans, förlustinstans, affärsinstans, 
        menyinstans;

    public void SpelaStridsMusik()
    {
         stridsinstans = LjudChef.Instans.SpelaLjud(stridsmusik);
    }

    public void SpelaSegerMusik()
    {
        LjudChef.Instans.SpelaEnskottsLjud(seger);
    }

    public void SpelaFörlustMusik()
    {
        LjudChef.Instans.SpelaEnskottsLjud(förlust);
    }

    public void SpelaAffärsMusik()
    {
        affärsinstans = LjudChef.Instans.SpelaLjud(affärsmusik);
    }

    public void SpelaMenyMusik()
    {
        menyinstans = LjudChef.Instans.SpelaLjud(menymusik);
    }
    
    public void StoppaStridsMusik()
    {
        LjudChef.Instans.StoppaLjud(stridsinstans);
    }

    public void StoppaSegerMusik()
    {
        LjudChef.Instans.StoppaLjud(segerinstans);
    }

    public void StoppaFörlustMusik()
    {
        LjudChef.Instans.StoppaLjud(förlustinstans);
    }

    public void StoppaAffärsMusik()
    {
        LjudChef.Instans.StoppaLjud(affärsinstans);
    }

    public void StoppaMenyMusik()
    {
        LjudChef.Instans.StoppaLjud(menyinstans);
    }
}
