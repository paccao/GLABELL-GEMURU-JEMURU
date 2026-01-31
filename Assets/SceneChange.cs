using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void ChangeScene()
    {
        Debug.Log("aaah");
        SceneManager.LoadScene(sceneName);
    }
}
