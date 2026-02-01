using FMODUnity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class UpgradeHoverSound : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public AffärsLjud UIL;
    public void OnPointerEnter(PointerEventData eventData)
    {
        UIL.uiSvävaInstans = RuntimeManager.CreateInstance(UIL.uiSväva);
        UIL.uiSvävaInstans.start();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIL.uiSvävaInstans.stop(STOP_MODE.ALLOWFADEOUT);
    }
}
