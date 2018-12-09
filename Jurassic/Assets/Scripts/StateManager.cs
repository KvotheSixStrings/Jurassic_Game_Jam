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
    public RawImage[] balls;

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
    public Text gameOverText;
    public Text messageToPlayerText;
    public string gameOverMessage;
    public string highScoreMessage;

    [Header("Game Over Settings")]
    public GameObject gameOverParticalSystem;
    public Transform gameOverPosition;
    public float waitTimeForGameOverText = 3f;
    public GameObject gameUi;

    void Start() {
        instance = this;
        if (PlayerPrefs.HasKey("HighScore")) {
            if (highScoreText)
                highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        }
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

    private void Update() {
        if (Input.GetKeyDown(KeyCode.U)) {
            BallLost();
        }   
    }

    public void BallLost() {
        ballInPlay = false;
        FindObjectOfType<SwitchFromTriggerToCollison>().collider.isTrigger = true;
        balls[numberOfBalls - currentBallCount].enabled = false;
        Debug.Log("In ball lost");
        if(currentBall)
            Destroy(currentBall);
        currentBallCount++;
        if (currentBallCount > numberOfBalls) {
            GameObject go = Instantiate(gameOverParticalSystem);
            go.transform.position = gameOverPosition.position;
            Invoke("GameOver", waitTimeForGameOverText);
        }
        else {
            SpawnBall();

        }
    }
    private void SpawnBall() {
        Debug.Log("in spawn ball");
        if (ballPrefab) {
            currentBall = Instantiate(ballPrefab);
            currentBall.transform.position = ballSpawnPosition.position;
        }
        else {
            Debug.LogWarning("Assign the Ball Prefab");
        }
    }
    private void GameOver() {
        gameUi.SetActive(false);
        if(gameOverText)
            gameOverText.text = gameOverMessage;
        CheckHighScore();
        Invoke("ResetGame", 5f);
    }

    private void CheckHighScore() {
        if (PlayerPrefs.HasKey("HighScore")) {
            int temp = PlayerPrefs.GetInt("HighScore");
            if (temp < score) {
                ChangeHighScore();
            }
        }
        else {
            ChangeHighScore();
        }
    }

    private void ChangeHighScore() {
        PlayerPrefs.SetInt("HighScore", score);
        if (highScoreText)
            highScoreText.text = score.ToString();
        if (messageToPlayerText)
            messageToPlayerText.text = highScoreMessage;
    }

    public void ResetGame() {
        gameUi.SetActive(true);
        score = 0;
        ResetMultiplier();
        currentBallCount = 1;
        foreach(RawImage go in balls) {
            go.enabled =true;
        }
        if (scoreText)
            scoreText.text = score.ToString();
        if (multiplierText)
            multiplierText.text = multiplierText.ToString();
        if (ballText)
            ballText.text = ballText.ToString();
        if (highScoreText)
            highScoreText.text = highScoreText.ToString();
        if (gameOverText)
            gameOverText.text = "";
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
