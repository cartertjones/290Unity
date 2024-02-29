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

    private bool gameActive;

    public TextMeshProUGUI player1ScoreText, player2ScoreText, restartText;

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

        gameActive = true;
    }

    private void Start() {
        player1Score = 0;
        player2Score = 0;
    }

    int prevP1Score, prevP2Score;
    public void Update() {
        if (prevP1Score != player1Score) {
            prevP1Score = player1Score;
            player1ScoreText.text = player1Score.ToString();
        }
        if (prevP2Score != player2Score) {
            prevP2Score = player2Score;
            player2ScoreText.text = player2Score.ToString();
        }
        if (player1Score >= 10 || player2Score >= 10) {
            gameActive = false;
            restartText.text = "Press 'R' to restart";

            if (player1Score >= 10) {
                player1ScoreText.text = "Player 1 Wins!";
                player2ScoreText.text = "";
            }
            else {
                player1ScoreText.text = "Player 2 Wins!";
                player2ScoreText.text = "";
            }

            //freeze all characters
            Movement[] players = FindObjectsOfType<Movement>();
            foreach (Movement player in players) {
                player.enabled = false;
            }
        }

        if(gameActive == false) {
            if(Input.GetKeyDown(KeyCode.R)) {
                player1Score = 0;
                player2Score = 0;
                player1ScoreText.text = player1Score.ToString();
                player2ScoreText.text = player2Score.ToString();
                restartText.text = "";
                gameActive = true;

                Managers.Inventory.ResetAllItems();

                //unfreeze all characters
                Movement[] players = FindObjectsOfType<Movement>();
                foreach (Movement player in players) {
                    player.enabled = true;
                }
            }
        }
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
