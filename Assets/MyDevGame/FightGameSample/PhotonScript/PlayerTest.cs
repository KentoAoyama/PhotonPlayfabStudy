using Fusion;
using UnityEngine;

public class PlayerTest : NetworkBehaviour
{
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            Debug.Log("�{�^���������ꂽ��");
        }
    }
}