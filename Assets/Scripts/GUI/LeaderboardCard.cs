using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardCard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private TextMeshProUGUI playerRankText;


    public void Init(string playerName, int score, int rank)
    {
        playerNameText.text = playerName;
        playerScoreText.text = score.ToString();
        playerRankText.text = rank.ToString();
    }
}
