using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public static InGameMenu Instance;
    private bool isPaused;
    public GameObject pauseParent;
    
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

    public void OnPause()
    {
        if (!isPaused)
        {
            Debug.Log("Pause");
            pauseParent.gameObject.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
            LjudChef.Instans.PausaMusiken(true);
        }
        else if (isPaused)
        {
            Debug.Log("Resume");
            Time.timeScale = 1;
            pauseParent.gameObject.SetActive(false);
            isPaused = false;
            LjudChef.Instans.PausaMusiken(false);
        }
    }
}
