using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LeaderBoard
{
    public List<LeaderBoardEntry> entries;

    public LeaderBoard()
    {
        entries = new List<LeaderBoardEntry>();
    }
}


[System.Serializable]
public class LeaderBoardEntry
{
    public string playerName;
    public int score;

    public LeaderBoardEntry(string playerName, int score)
    {
        this.playerName = playerName;
        this.score = score;
    }
}