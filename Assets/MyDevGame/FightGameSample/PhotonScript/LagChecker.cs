using Fusion;
using System;
using UnityEngine;

public class LagChecker : NetworkBehaviour
{
    [Networked(OnChanged = nameof(LagCheck))]
    private DateTime SenderTime { get; set; }

    public void SendTime()
    {
        SenderTime = DateTime.Now;
        Debug.Log($"タイマー更新！！：{SenderTime}");
    }

    public static void LagCheck(Changed<LagChecker> changed)
    {
        changed.Behaviour.LagCheck();
    }

    private void LagCheck()
    {
        var elapsedTime = DateTime.Now - SenderTime;
        // 経過時間をミリ秒に変換して表示
        long milliseconds = (long)elapsedTime.TotalMilliseconds;
        Debug.Log("経過時間（ミリ秒）: " + milliseconds);
    }
}
