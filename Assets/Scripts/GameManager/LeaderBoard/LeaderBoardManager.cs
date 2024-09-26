using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class LeaderBoardManager
{
    private LeaderBoard leaderboard;
    private string filePath = FileManager.savPath + "/leaderboard.djayson";

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

    public List<LeaderBoardEntry> GetScoresRanked()
    {
        leaderboard.entries.Sort((LeaderBoardEntry o1, LeaderBoardEntry o2) =>
        {
            return -o1.score.CompareTo(o2.score);
        });

        List<LeaderBoardEntry> top = new List<LeaderBoardEntry>();
        for (int i = 0; i < 10; i++)
        {
            if (leaderboard.entries.Count <= i) break;
            top.Add(leaderboard.entries[i]);
        }

        return leaderboard.entries;
    }

    public void AddScore(string playerName, int score)
    {
        leaderboard.entries.Add(new LeaderBoardEntry(playerName, score));
        FileManager.SaveJSON(filePath, leaderboard);
    }
}
