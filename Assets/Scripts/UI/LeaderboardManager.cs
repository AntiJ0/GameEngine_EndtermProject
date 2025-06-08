using System.Collections.Generic;
using UnityEngine;

public static class LeaderboardManager
{
    private const string Key = "Leaderboard";

    public static List<int> LoadScores()
    {
        string json = PlayerPrefs.GetString(Key, "");
        if (string.IsNullOrEmpty(json))
            return new List<int>();

        return JsonUtility.FromJson<ScoreListWrapper>(json).scores;
    }

    public static void SaveScore(int newScore)
    {
        List<int> scores = LoadScores();
        scores.Add(newScore);
        scores.Sort((a, b) => b.CompareTo(a)); // 내림차순 정렬

        if (scores.Count > 5)
            scores = scores.GetRange(0, 5);

        ScoreListWrapper wrapper = new ScoreListWrapper { scores = scores };
        string json = JsonUtility.ToJson(wrapper);
        PlayerPrefs.SetString(Key, json);
        PlayerPrefs.Save();
    }

    [System.Serializable]
    private class ScoreListWrapper
    {
        public List<int> scores;
    }
}