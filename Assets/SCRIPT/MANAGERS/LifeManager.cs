using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public static LifeManager Instance;
    public Image[] hearts;

    public Sprite fullHeart;

    public Sprite emptyHeart;

    public int maxLives = 5;
    private int currentLives;



    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentLives = maxLives;

        UpdateHearts();

    }

    public void LoseLife()
    {
        currentLives--;

        UpdateHearts();

        Debug.Log("Lives Left : " + currentLives);

        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentLives)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
    void GameOver()
    {
        Debug.Log("GAME OVER");

        if (GameOverManager.Instance != null)
        {
            GameOverManager.Instance.ShowGameOver();
        }
    } 

    public int GetLives()
    {
        return currentLives;
    }
    
}