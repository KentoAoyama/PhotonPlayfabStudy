using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;

public class PlayfabRankingExample : MonoBehaviour
{
    [SerializeField]
    private int _score = 500;

    void Start()
    {
        PlayFabClientAPI.LoginWithCustomID(
            new LoginWithCustomIDRequest
            {
                TitleId = PlayFabSettings.TitleId,
                CustomId = Random.Range(1, 100).ToString(),
                CreateAccount = true
            },
            result =>
            {
                Debug.Log("ログイン成功！");
                SubmitScore(_score);
                GetLeaderboard();

            },
            error =>
            {
                Debug.LogError(error.GenerateErrorReport());
            });
    }

    /// <summary>
    /// スコアをランキングに登録する
    /// </summary>
    /// <param name="score">登録するスコア</param>
    private void SubmitScore(int score)
    {
        PlayFabClientAPI.UpdatePlayerStatistics(
            new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>
                {
                    new StatisticUpdate
                    {
                        StatisticName = "Ranking",
                        Value = score
                    }
                }
            },
            result =>
            {
                Debug.Log($"スコア：{score}を送信完了!!");
            },
            error =>
            {
                Debug.LogError(error.GenerateErrorReport());
            });
    }

    /// <summary>
    /// ランキングの情報を取得する
    /// </summary>
    private void GetLeaderboard()
    {
        PlayFabClientAPI.GetLeaderboard(
            new GetLeaderboardRequest
            {
                StatisticName = "Ranking"
            },
            result =>
            {
                foreach (var item in result.Leaderboard)
                {
                    Debug.Log($"{item.Position + 1}位:{item.DisplayName} " + $"スコア {item.StatValue}");
                }
            },
            error =>
            {
                Debug.Log(error.GenerateErrorReport());
            });
    }
}
