using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private LeaderBoardManager leaderboard;
    public int currentScore { get; private set; }
    public string currentUsername { get; private set; }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            currentUsername = "Felix";
            currentScore = 0;
            leaderboard = new LeaderBoardManager();
            DontDestroyOnLoad(gameObject);
        }
    }

    public List<LeaderBoardEntry> GetLeaderBoard()
    {
        return leaderboard.GetScoresRanked();
    }

    public void SetCurrentScore(int score)
    {
        currentScore = score;
    }

    public void SetCurrentName(string name)
    {
        currentUsername = name;
    }

    public void GoToMainScene()
    {
        SceneManager.LoadScene("ScrollerTest");
    }

    public void GoToEndScene()
    {
        SceneManager.LoadScene("EndScene");
    }

    public void GoToStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void AddDataToLeaderBoard()
    {
        leaderboard.AddScore(currentUsername, currentScore);
    }
}
