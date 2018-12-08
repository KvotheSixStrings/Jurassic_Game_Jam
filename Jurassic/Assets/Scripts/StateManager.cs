using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour {
    public static StateManager instance;

    [Header("Ball Setup")]
    public GameObject ballPrefab;
    private GameObject currentBall;
    public Transform ballSpawnPosition;

    [Header("Ball Controls")]
    public int numberOfBalls = 3;
    public int currentBallCount = 1;
    public bool ballInPlay = false;

    [Header("Score Controls")]
    public int score = 0;
    public int multiplier = 1;

    [Header("Multiplier Controls")]
    public float intervalToIncreaseMultiplier = 10f;
    public int amountToIncreaseMultiplierPerInterval = 1;
    private int defaultMultiplier = 1;

    [Header("Text Controls")]
    public Text scoreText;
    public Text multiplierText;
    public Text ballText;
    public Text highScoreText;
    public Text messageToPlayerText;
    public string gameOverMessage;
    public float timeToWaitToSwitchHighScore = 3.5f;
    public string highScoreMessage;

    void Start() {
        instance = this;
        SpawnBall();
        InvokeRepeating("ConstantMultiplierAdd", intervalToIncreaseMultiplier, intervalToIncreaseMultiplier);
    }

    public void AddToScore(int val) {
        score = score + val * multiplier;
        if (scoreText)
            scoreText.text = score.ToString();
    }

    public void SubtractFromScore(int val) {
        score = score - val * multiplier;
        if (scoreText)
            scoreText.text = score.ToString();

    }

    public void AddToMultiplier(int val) {
        multiplier += val;
        if (multiplierText)
            multiplierText.text = multiplierText.ToString();

    }

    public void SubtractFromMultiplier(int val) {
        multiplier -= val;
        if (multiplierText)
            multiplierText.text = multiplierText.ToString();

    }

    public void ResetMultiplier() {
        multiplier = defaultMultiplier;
        if (multiplierText)
            multiplierText.text = multiplierText.ToString();

    }

    public void BallLost() {
        ballInPlay = false;
        if(currentBall)
            Destroy(currentBall);
        currentBallCount++;
        if (currentBallCount > numberOfBalls) {
            GameOver();
        }
        else {
            if (ballText)
                ballText.text = ballText.ToString();
            SpawnBall();

        }
    }
    private void SpawnBall() {
        if (ballPrefab) {
            currentBall = Instantiate(ballPrefab);
            currentBall.transform.position = ballSpawnPosition.position;
        }
        else {
            Debug.LogWarning("Assign the Ball Prefab");
        }
    }
    private void GameOver() {
        if(messageToPlayerText)
            messageToPlayerText.text = gameOverMessage;
        CheckHighScore();
    }

    private void CheckHighScore() {
        if (PlayerPrefs.HasKey("HighScore")) {
            int temp = PlayerPrefs.GetInt("HighScore");
            if (temp < score) {
                Invoke("ChangeHighScore", timeToWaitToSwitchHighScore);                
            }
        }
        else {
            Invoke("ChangeHighScore", timeToWaitToSwitchHighScore);
        }
    }

    private void ChangeHighScore() {
        PlayerPrefs.SetInt("HighScore", score);
        if (highScoreText)
            highScoreText.text = highScoreText.ToString();
        if (messageToPlayerText)
            messageToPlayerText.text = highScoreMessage;
    }

    public void ResetGame() {
        score = 0;
        ResetMultiplier();
        currentBallCount = 1;
        if (scoreText)
            scoreText.text = score.ToString();
        if (multiplierText)
            multiplierText.text = multiplierText.ToString();
        if (ballText)
            ballText.text = ballText.ToString();
        if (highScoreText)
            highScoreText.text = highScoreText.ToString();
        if (messageToPlayerText)
            messageToPlayerText.text = "";

    }

    public void ExitGame() {
        Application.Quit();
    }

    private void ConstantMultiplierAdd() {
        if(ballInPlay)
            AddToMultiplier(amountToIncreaseMultiplierPerInterval);
    }
}
