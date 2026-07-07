using TMPro;
using UnityEngine;

public class CountdownUI : MonoBehaviour
{
    public static CountdownUI Instance;

    public TextMeshProUGUI countdownText;

    void Awake()
    {
        Instance = this;

        countdownText.gameObject.SetActive(false);
    }

    public void ShowNumber(int number)
    {
        countdownText.gameObject.SetActive(true);

        countdownText.text = number.ToString();
    }

    public void Hide()
    {
        countdownText.gameObject.SetActive(false);
    }
}