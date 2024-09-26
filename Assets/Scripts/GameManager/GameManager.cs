using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private LeaderBoardManager leaderboard;
    [SerializeField] private ScrollerManager scrollerManager;

    void Awake()
    {
        instance = this;
        leaderboard = new LeaderBoardManager();
    }

    public void SetCurrentScrollerSpeed(float percentage)
    {
        scrollerManager.SetCurrentSpeed(percentage);
    }

    public float GetScrollSpeed()
    {
        return scrollerManager.GetCurrentScrollSpeed();
    }


}
