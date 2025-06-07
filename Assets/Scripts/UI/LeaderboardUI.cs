using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LeaderboardUI : MonoBehaviour
{
    public TextMeshProUGUI[] scoreTexts; 

    void Start()
    {
        List<int> scores = LeaderboardManager.LoadScores();
        for (int i = 0; i < scoreTexts.Length; i++)
        {
            if (i < scores.Count)
                scoreTexts[i].text = $"{i + 1}. {scores[i]}";
            else
                scoreTexts[i].text = $"{i + 1}. ---";
        }
    }
}