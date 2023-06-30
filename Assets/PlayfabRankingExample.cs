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
                Debug.Log("���O�C�������I");
                SubmitScore(_score);
                GetLeaderboard();

            },
            error =>
            {
                Debug.LogError(error.GenerateErrorReport());
            });
    }

    /// <summary>
    /// �X�R�A�������L���O�ɓo�^����
    /// </summary>
    /// <param name="score">�o�^����X�R�A</param>
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
                Debug.Log($"�X�R�A�F{score}�𑗐M����!!");
            },
            error =>
            {
                Debug.LogError(error.GenerateErrorReport());
            });
    }

    /// <summary>
    /// �����L���O�̏����擾����
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
                    Debug.Log($"{item.Position + 1}��:{item.DisplayName} " + $"�X�R�A {item.StatValue}");
                }
            },
            error =>
            {
                Debug.Log(error.GenerateErrorReport());
            });
    }
}
