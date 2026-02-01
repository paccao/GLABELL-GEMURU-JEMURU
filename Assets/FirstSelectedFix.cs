using UnityEngine;
using UnityEngine.EventSystems;

public class FirstSelectedFix : MonoBehaviour
{
    public RectTransform ui;
    private EventSystem eventSystem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventSystem = EventSystem.current;
        
        eventSystem.SetSelectedGameObject(ui.gameObject);
    }

}
