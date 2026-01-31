using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

public class MixerSliderLink : MonoBehaviour
{
    [SerializeField] private string vcaPath;
    private Slider slider;
    [SerializeField] public VCA vca;

    public void Start()
    {
        vca = RuntimeManager.GetVCA(vcaPath);
        slider ??= GetComponent<Slider>();

        if (!vca.isValid())
        {
            Debug.LogError("vca är invalid!");
        }

        vca.getVolume(out float volume);
        slider.value = volume;
    }

    public void NärVärdetÄndras(float värde)
    {
        vca.setVolume(värde);
    }
}
