using FMODUnity;
using UnityEngine;

[CreateAssetMenu(fileName = "IntroLjud", menuName = "Scriptable Objects/IntroLjud")]
public class IntroLjud : ScriptableObject
{
    public EventReference nivåStart, fiskelina, vattenplask;

    public void SpelaNivåstartljud()
    {
        LjudChef.Instans.SpelaEnskottsLjud(nivåStart);
    }
    
    public void SpelaFiskelineljud()
    {
        LjudChef.Instans.SpelaEnskottsLjud(fiskelina);
    }
    
    public void SpelaVattenplask()
    {
        LjudChef.Instans.SpelaEnskottsLjud(vattenplask);
    }
}
