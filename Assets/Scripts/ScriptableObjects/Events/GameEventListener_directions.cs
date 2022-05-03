using System;
using UnityEngine.Events;
using UnityEngine;

[Serializable]
public class Event_directions : UnityEvent<Directions>
{

}

public class GameEventListener_directions : MonoBehaviour
{
    public GameEvent_directions Event;
    public Event_directions Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(Directions sender)
    {
        Response.Invoke(sender);
    }
}
