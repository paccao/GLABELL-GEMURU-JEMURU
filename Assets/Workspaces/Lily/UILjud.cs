using FMODUnity;
using UnityEngine;

[CreateAssetMenu(fileName = "UILjud", menuName = "Scriptable Objects/UILjud")]
public class UILjud : ScriptableObject
{
    public EventReference uiKlick, uiSväva;

    public void Klick()
    {
        LjudChef.Instans.SpelaEnskottsLjud(uiKlick);
    }

    public void Sväva()
    {
        LjudChef.Instans.SpelaEnskottsLjud(uiSväva);
    }
}
