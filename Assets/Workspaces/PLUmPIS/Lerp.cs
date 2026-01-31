using UnityEngine;
using Workspaces.Joel.Assets.Scripts;

public class Lerp : MonoBehaviour
{
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;
    private float duration;
    private float time = 0f;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        duration = GameManager.Instance.gameDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            t = Mathf.Clamp01(t);
            
            transform.position = Vector3.Lerp(startPos.position, endPos.position, t);
        }

        else
        {
            transform.position = endPos.position;
        }
    }
}
