using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugToggle : MonoBehaviour
{
    [SerializeField] 
    private GameObject _debugWindow;

    public void CloseDebugWindow(bool val)
    {
        _debugWindow.SetActive(val);
    }
}
