using UnityEngine;
using TMPro;
using System.Collections;

public class QuoteUI : MonoBehaviour
{
    public static QuoteUI Instance;

    public GameObject panel;
    public TextMeshProUGUI quoteText;

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public IEnumerator ShowQuote(string text)
    {
        panel.SetActive(true);

        quoteText.text = text;

        yield return new WaitForSeconds(2.5f);

        panel.SetActive(false);
    }
}