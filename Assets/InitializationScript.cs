using UnityEngine;

public class InitializationScript : MonoBehaviour
{
    public void Awake()
    {
        if (PlayerPrefs.HasKey("Target_FrameRate"))
        {
            Application.targetFrameRate = PlayerPrefs.GetInt("Target_FrameRate");
            return;
        }
        
        Application.targetFrameRate = 60;
    }
}
