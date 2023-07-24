using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCanvasCamera : MonoBehaviour
{
    void Start()
    {
        var canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
    }
}
