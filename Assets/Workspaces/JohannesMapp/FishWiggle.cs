using UnityEngine;

public class FishWiggle : MonoBehaviour
{
    public Transform[] maskParts;
    public float wiggleSpeed = 5f;
    public float wiggleAmount = 0.0005f;

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
                startPositions[i] + Vector3.up * offset;
        }
    }
}
