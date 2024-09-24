using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LeaderBoardManager
{
    private LeaderBoard leaderboard;
    private string filePath = FileManager.dataPath + "/leaderboard.djayson";

    public LeaderBoardManager()
    {
        leaderboard = new LeaderBoard();

        if (File.Exists(filePath))
        {
            leaderboard = FileManager.LoadJSON<LeaderBoard>(filePath);
        }
        else
        {
            leaderboard = new LeaderBoard();
        }
    }

    public List<LeaderBoardEntry> GetScores()
    {
        return leaderboard.entries;
    }

    public void AddScore(string playerName, int score)
    {
        leaderboard.entries.Add(new LeaderBoardEntry(playerName, score));
    }
}
