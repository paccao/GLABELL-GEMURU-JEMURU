using FMODUnity;
using UnityEngine;

[CreateAssetMenu(fileName = "FiskLjud", menuName = "Scriptable Objects/FiskLjud")]
public class FiskLjud : ScriptableObject
{
    public EventReference dö, träff;

    public void SpelaDöLjud()
    {
        LjudChef.Instans.SpelaEnskottsLjud(dö);
    }

    public void SpelaTräffLjud()
    {
        LjudChef.Instans.SpelaEnskottsLjud(träff);
    }
}
