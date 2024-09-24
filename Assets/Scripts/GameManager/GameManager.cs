using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private LeaderBoardManager leaderboard;

    void Awake()
    {
        instance = this;
        leaderboard = new LeaderBoardManager();
    }


}
