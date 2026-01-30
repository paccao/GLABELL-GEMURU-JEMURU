using FMODUnity;
using UnityEngine;

[CreateAssetMenu(fileName = "MaskLjud", menuName = "Scriptable Objects/MaskLjud")]
public class MaskLjud : ScriptableObject
{
    public EventReference stön;
    public EventReference slagMiss;
    public EventReference slagTräff;
    public EventReference dö;

    public void SpelaStönLjud()
    {
        LjudChef.Instans.SpelaEnskottsLjud(stön);
    }

    public void SpelaSlagMiss()
    {
        LjudChef.Instans.SpelaEnskottsLjud(slagMiss);
    }

    public void SpelaSlagTräff()
    {
        LjudChef.Instans.SpelaEnskottsLjud(slagTräff);
    }

    public void SpelaDöLjud()
    {
        LjudChef.Instans.SpelaEnskottsLjud(dö);
    }
}
