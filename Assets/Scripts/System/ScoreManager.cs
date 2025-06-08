using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public float score = 0f;
    public int currentScore = 0;
    public bool isGameOver = false;

    private float playerSpeed = 5f; // PlayerController에서 매 프레임 전달
    private float scoreMultiplier = 1f;

    private void Update()
    {
        if (isGameOver || Time.timeScale == 0f) return;

        score += playerSpeed * Time.deltaTime;
        currentScore = Mathf.FloorToInt(score);
        scoreText.text = "Score: " + currentScore;
    }

    public void SetGameOver()
    {
        isGameOver = true;
    }

    public int GetFinalScore()
    {
        return currentScore;
    }

    public void SetSpeed(float speed)
    {
        playerSpeed = speed;
    }

    public void SetScoreMultiplier(float multiplier)
    {
        scoreMultiplier = multiplier;
    }
}