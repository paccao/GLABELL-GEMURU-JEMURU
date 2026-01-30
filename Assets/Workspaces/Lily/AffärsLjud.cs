using FMODUnity;
using UnityEngine;

[CreateAssetMenu(fileName = "Affärsljud", menuName = "Scriptable Objects/Affärsljud")]
public class AffärsLjud : ScriptableObject
{
    public EventReference uiSväva, uiKlick, köp, nekatKöp, affärPopup, affärStäng;

    public void SpelaUiSväva()
    {
        LjudChef.Instans.SpelaEnskottsLjud(uiSväva);
    }
    
    public void SpelaUiKlick()
    {
        LjudChef.Instans.SpelaEnskottsLjud(uiKlick);
    }
    
    public void SpelaKöpLjud()
    {
        LjudChef.Instans.SpelaEnskottsLjud(köp);
    }

    public void SpelaNekatKöpLjud()
    {
        LjudChef.Instans.SpelaEnskottsLjud(nekatKöp);
    }
    
    public void SpelaAffärPopup()
    {
        LjudChef.Instans.SpelaEnskottsLjud(affärPopup);
    }
    
    public void SpelaAffärStäng()
    {
        LjudChef.Instans.SpelaEnskottsLjud(affärStäng);
    }
}
