using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public float score = 0f;
    public int currentScore = 0;
    public bool isGameOver = false;

    public float playerSpeed = 5f; // PlayerController에서 가져와 덮어쓸 예정

    private void Update()
    {
        if (isGameOver) return;

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
}