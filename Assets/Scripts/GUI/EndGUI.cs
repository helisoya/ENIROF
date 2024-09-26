using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGUI : MonoBehaviour
{
    [Header("Leaderboard")]
    [SerializeField] private Transform leaderboardRoot;
    [SerializeField] private Transform leaderboardCardPrefab;

    [Header("Your Score")]
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI playerScoreText;


    void Start()
    {
        GameManager.instance.AddDataToLeaderBoard();

        playerNameText.text = GameManager.instance.currentUsername;
        playerScoreText.text = GameManager.instance.currentScore.ToString();

        InitLeaderBoard();
    }

    void InitLeaderBoard()
    {

    }

    public void Click_ToStartScene()
    {
        GameManager.instance.GoToStartScene();
    }
}
