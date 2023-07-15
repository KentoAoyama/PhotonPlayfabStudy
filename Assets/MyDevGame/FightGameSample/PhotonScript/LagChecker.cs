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
        Debug.Log($"�^�C�}�[�X�V�I�I�F{SenderTime}");
    }

    public static void LagCheck(Changed<LagChecker> changed)
    {
        changed.Behaviour.LagCheck();
    }

    private void LagCheck()
    {
        var elapsedTime = DateTime.Now - SenderTime;
        // �o�ߎ��Ԃ��~���b�ɕϊ����ĕ\��
        long milliseconds = (long)elapsedTime.TotalMilliseconds;
        Debug.Log("�o�ߎ��ԁi�~���b�j: " + milliseconds);
    }
}
