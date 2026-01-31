using FMODUnity;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "MaskLjud", menuName = "Scriptable Objects/MaskLjud")]
public class MaskLjud : ScriptableObject
{
    public EventReference damage;
    public EventReference slag;
    public EventReference dö;
    public EventReference dash;

    public void SpelaDashLjud()
    {
        LjudChef.Instans.SpelaEnskottsLjud(dash);
    }

    public void PlayDamageSound()
    {
        LjudChef.Instans.SpelaEnskottsLjud(damage);
    }

    public void SpelaSlag()
    {
        LjudChef.Instans.SpelaEnskottsLjud(slag);
    }

    public void SpelaDöLjud()
    {
        LjudChef.Instans.SpelaEnskottsLjud(dö);
    }
}
