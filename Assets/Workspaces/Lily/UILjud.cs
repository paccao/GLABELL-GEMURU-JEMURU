using FMODUnity;
using UnityEngine;

[CreateAssetMenu(fileName = "UILjud", menuName = "Scriptable Objects/UILjud")]
public class UILjud : ScriptableObject
{
    public EventReference uiKlick, uiSväva, uiStart;

    public void Klick()
    {
        LjudChef.Instans.SpelaEnskottsLjud(uiKlick);
    }

    public void Sväva()
    {
        LjudChef.Instans.SpelaEnskottsLjud(uiSväva);
    }

    public void StartaSpelet()
    {
        RuntimeManager.PlayOneShot(uiStart);
    }
}
