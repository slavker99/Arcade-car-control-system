using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameInputController : MonoBehaviour
{
    public UnityEvent LeftMovEvent = new UnityEvent();
    public UnityEvent RightMovEvent = new UnityEvent();

    private void Update()
    {
        InputControlling();
    }

    private void InputControlling()
    {
        if (Input.GetKeyDown("left") || Input.GetKeyDown("a"))
            LeftMovEvent.Invoke();
        if (Input.GetKeyDown("right") || Input.GetKeyDown("d"))
            RightMovEvent.Invoke();
    }
}
