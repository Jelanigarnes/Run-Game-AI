using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// Handles the loading and saving of the ScoreBoard
/// Nice to have in future saving the scoreboard online
/// </summary>
public class ScoreManager
{
    // name of file
    private string _fileName = "RunGameScores.dat";

    /// <summary>
    /// Preload list of the data from scores
    /// </summary>
    public List<PlayerScore> playerScores = new List<PlayerScore>();
    public void SaveScores()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/" + _fileName);
        bf.Serialize(file, playerScores);
        file.Close();
    }

    public void LoadScores()
    {
        if (File.Exists(Application.dataPath + "/" + _fileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/" + _fileName, FileMode.Open);
            playerScores = (List<PlayerScore>)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            playerScores.Add(new PlayerScore("Jayce", Random.Range(1000f, 1000000f)));
            playerScores.Add(new PlayerScore("Jelani", Random.Range(1000f, 1000000f)));
            playerScores.Add(new PlayerScore("Adrian", Random.Range(1000f, 1000000f)));

            SaveScores();
        }
    }

    public void AddScore(string playerName, float score)
    {
        playerScores.Add(new PlayerScore(playerName, score));
        SaveScores();
    }
    public List<PlayerScore> GetTop10Scores()
    {
        playerScores.Sort((x, y) => y.Score.CompareTo(x.Score));
        return playerScores.GetRange(0, Mathf.Min(playerScores.Count, 10));
    }
}

[System.Serializable]
public class PlayerScore
{
    public string PlayerName;
    public float Score;

    public PlayerScore(string playerName, float score)
    {
        this.PlayerName = playerName;
        this.Score = score;
    }
}
