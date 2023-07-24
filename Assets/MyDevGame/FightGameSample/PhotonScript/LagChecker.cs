using Fusion;
using System;
using UnityEngine;

public class LagChecker : NetworkBehaviour
{
    [Networked(OnChanged = nameof(LagCheck))]
    [Capacity(23)]
    private string SenderTime { get; set; }

    public void SendTime()
    {
        var currentTime = DateTime.Now;

        //DateTime.Nowを出力すると　年/月/日 時間:分:秒　
        //という形になるので　Millisecondも後ろに追加する
        SenderTime = $"{currentTime}:{currentTime.Millisecond}";
        Debug.Log($"タイマー更新！！：{SenderTime}");
    }

    public static void LagCheck(Changed<LagChecker> changed)
    {
        changed.Behaviour.LagCheck();
    }

    private void LagCheck()
    {
        //変更された時間をStringからDateTimeに変換する
        var timeData = SenderTime.Split();
        var date = Array.ConvertAll(timeData[0].Split('/'), int.Parse);
        var time = Array.ConvertAll(timeData[1].Split(':'), int.Parse);
        DateTime senderTime = new(date[0],  date[1],  date[2],  time[0],  time[1], time[2], time[3]);

        var elapsedTime = DateTime.Now - senderTime;
        //経過時間をミリ秒に変換して表示
        long milliseconds = (long)elapsedTime.TotalMilliseconds;
        Debug.Log("経過時間（ミリ秒）: " + milliseconds);
    }
}
