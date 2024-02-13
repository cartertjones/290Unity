using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int player1Score, player2Score;
    public int scoreIncreaseAmount;

    public TextMeshProUGUI player1ScoreText, player2ScoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() {
        player1Score = 0;
        player2Score = 0;
    }

    public void IncreasePlayer1Score()
    {
        player1Score += scoreIncreaseAmount;
        player1ScoreText.text = player1Score.ToString();
        
    }

    public void IncreasePlayer2Score()
    {
        player2Score += scoreIncreaseAmount;
        player2ScoreText.text = player2Score.ToString();
    }
}
