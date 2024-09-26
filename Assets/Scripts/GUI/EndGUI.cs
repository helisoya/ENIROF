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
        List<LeaderBoardEntry> entries = GameManager.instance.GetLeaderBoard();
        for (int i = 0; i < entries.Count; i++)
        {
            Instantiate(leaderboardCardPrefab, leaderboardRoot).GetComponent<LeaderboardCard>()
                .Init(entries[i].playerName, entries[i].score, i + 1);
        }

        leaderboardRoot.GetComponent<RectTransform>().sizeDelta = new Vector2(
            leaderboardRoot.GetComponent<RectTransform>().sizeDelta.x,
            (leaderboardCardPrefab.GetComponent<RectTransform>().sizeDelta.y + 5) * entries.Count
        );
    }

    public void Click_ToStartScene()
    {
        GameManager.instance.GoToStartScene();
    }
}
