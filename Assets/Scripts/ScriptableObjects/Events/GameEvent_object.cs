using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent_Object", menuName = "GameEvent/Objects")]
public class GameEvent_object : ScriptableObject
{
    private List<GameEventListener_object> listeners =
        new List<GameEventListener_object>();

    public void Raise(GameObject sender)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(sender);
    }

    public void RegisterListener(GameEventListener_object listener)
    { listeners.Add(listener); }

    public void UnregisterListener(GameEventListener_object listener)
    { listeners.Remove(listener); }
}