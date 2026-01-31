using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public static InGameMenu Instance;
    
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
    }
}
