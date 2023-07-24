using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTest : NetworkBehaviour
{
    [SerializeField]
    private Text _text;

    private LagChecker _lagCheck;

    [Networked] 
    private TickTimer _delay { get; set; }

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
                _delay = TickTimer.CreateFromSeconds(Runner, 0.5f);
            }
        }

        if (_delay.ExpiredOrNotRunning(Runner))
        {
            _text.text = "delay‚È‚µ";
        }
        else
        {
            _text.text = "delay’†";
        }
    }
}