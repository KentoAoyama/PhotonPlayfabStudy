using Fusion;
using UnityEngine;

public class PlayerTest : NetworkBehaviour
{
    private LagChecker _lagCheck;

    private void Start()
    {
        
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            if ((data.buttons & NetworkInputData.SPACEBUTTON1) != 0)
            {
                if (_lagCheck == null)
                {
                    _lagCheck = FindFirstObjectByType<LagChecker>().GetComponent<LagChecker>();
                }

                _lagCheck.SendTime();
            }
        }
    }
}