using UnityEngine;

public class MaskWiggle : MonoBehaviour
{
    public Transform[] maskParts;
    public float wiggleSpeed = 3f;
    public float wiggleAmount = 0.2f;

    Vector3[] startPositions;

    void Start()
    {
        startPositions = new Vector3[maskParts.Length];

        for (int i = 0; i < maskParts.Length; i++)
        {
            startPositions[i] = maskParts[i].localPosition;
        }
    }

    void Update()
    {
        for (int i = 0; i < maskParts.Length; i++)
        {
            float offset = Mathf.Sin(Time.time * wiggleSpeed + i) * wiggleAmount;

            maskParts[i].localPosition =
                startPositions[i] + Vector3.right * offset;
        }
    }
}