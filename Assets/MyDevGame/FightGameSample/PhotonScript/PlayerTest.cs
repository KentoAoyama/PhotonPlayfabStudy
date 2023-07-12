using Fusion;
using UnityEngine;

public class PlayerTest : NetworkBehaviour
{
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            Debug.Log("ƒ{ƒ^ƒ“‚ª‰Ÿ‚³‚ê‚½‚æ");
        }
    }
}