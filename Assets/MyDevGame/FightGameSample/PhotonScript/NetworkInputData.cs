using Fusion;
using UnityEngine;

public struct NetworkInputData : INetworkInput
{
    public const byte SPACEBUTTON1 = 0x01;

    public byte buttons;
}