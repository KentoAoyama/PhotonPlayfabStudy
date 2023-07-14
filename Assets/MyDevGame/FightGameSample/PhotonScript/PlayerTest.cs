using Fusion;
using UnityEngine;

public class PlayerTest : NetworkBehaviour
{
    private LagChecker _lagCheck;

    private void Start()
    {
        _lagCheck = FindFirstObjectByType<LagChecker>().GetComponent<LagChecker>();
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            if ((data.buttons & NetworkInputData.SPACEBUTTON1) != 0)
            {
                Debug.Log("ƒ{ƒ^ƒ“‚ª‰Ÿ‚³‚ê‚½‚æ");
            }
        }
    }
}