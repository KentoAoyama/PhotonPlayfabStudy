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

        //DateTime.Now���o�͂���Ɓ@�N/��/�� ����:��:�b�@
        //�Ƃ����`�ɂȂ�̂Ł@Millisecond�����ɒǉ�����
        SenderTime = $"{currentTime}:{currentTime.Millisecond}";
        Debug.Log($"�^�C�}�[�X�V�I�I�F{SenderTime}");
    }

    public static void LagCheck(Changed<LagChecker> changed)
    {
        changed.Behaviour.LagCheck();
    }

    private void LagCheck()
    {
        //�ύX���ꂽ���Ԃ�String����DateTime�ɕϊ�����
        var timeData = SenderTime.Split();
        var date = Array.ConvertAll(timeData[0].Split('/'), int.Parse);
        var time = Array.ConvertAll(timeData[1].Split(':'), int.Parse);
        DateTime senderTime = new(date[0],  date[1],  date[2],  time[0],  time[1], time[2], time[3]);

        var elapsedTime = DateTime.Now - senderTime;
        //�o�ߎ��Ԃ��~���b�ɕϊ����ĕ\��
        long milliseconds = (long)elapsedTime.TotalMilliseconds;
        Debug.Log("�o�ߎ��ԁi�~���b�j: " + milliseconds);
    }
}
