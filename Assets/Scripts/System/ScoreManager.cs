using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public float score = 0f;
    public int currentScore = 0;
    public bool isGameOver = false;

    private float playerSpeed = 5f;
    private float scoreMultiplier = 1f;

    private int lastScoreInt = 0;  // 마지막 골드 추가 시점의 정수 점수

    private void Update()
    {
        if (isGameOver || Time.timeScale == 0f) return;

        score += playerSpeed * scoreMultiplier * Time.deltaTime;
        currentScore = Mathf.FloorToInt(score);
        scoreText.text = "Score: " + currentScore;

        // 정수 점수가 이전보다 커졌을 때 그 차이만큼 골드 추가
        if (currentScore > lastScoreInt)
        {
            int diff = currentScore - lastScoreInt;
            GoldManager.Instance.AddGold(diff);
            lastScoreInt = currentScore;
        }
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