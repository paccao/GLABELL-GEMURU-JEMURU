using TMPro;
using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    [SerializeField] private TMP_Text hajScore;

    void Start()
    {
        hajScore.text = CurrencyManager.Instance.currentScore.ToString();
    }

}
