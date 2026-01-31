using FMODUnity;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "MaskLjud", menuName = "Scriptable Objects/MaskLjud")]
public class MaskLjud : ScriptableObject
{
    public EventReference groan;
    public EventReference slagMiss;
    public EventReference slagTräff;
    public EventReference dö;

    public void PlayGroanSound()
    {
        LjudChef.Instans.SpelaEnskottsLjud(groan);
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
