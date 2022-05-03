using System;
using UnityEngine.Events;
using UnityEngine;

[Serializable]
public class Event_object : UnityEvent<GameObject>
{

}

public class GameEventListener_object : MonoBehaviour
{
    public GameEvent_object Event;
    public Event_object Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(GameObject sender)
    {
        Response.Invoke(sender);
    }
}
