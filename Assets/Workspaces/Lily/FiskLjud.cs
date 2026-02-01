using FMODUnity;
using UnityEngine;

[CreateAssetMenu(fileName = "FiskLjud", menuName = "Scriptable Objects/FiskLjud")]
public class FiskLjud : ScriptableObject
{
    public EventReference die, hit, hitmask;

    public void SpelaDöLjud()
    {
        RuntimeManager.PlayOneShot(die);
    }

    public void SpelaTräffLjud()
    {
        RuntimeManager.PlayOneShot(hit);
    }
    public void SpelaMaskTräffLjud()
    {
        RuntimeManager.PlayOneShot(hitmask);
    }
}
