using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonOnHoverScript : MonoBehaviour, IPointerEnterHandler
{
    public UnityEvent OnHover;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHover.Invoke();
    }
}
