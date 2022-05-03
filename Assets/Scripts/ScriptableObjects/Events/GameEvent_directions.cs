using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent_Direcions", menuName = "GameEvent/Directions")]
public class GameEvent_directions : ScriptableObject
{
    private List<GameEventListener_directions> listeners =
        new List<GameEventListener_directions>();

    public void Raise(Directions sender)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(sender);
    }

    public void RegisterListener(GameEventListener_directions listener)
    { listeners.Add(listener); }

    public void UnregisterListener(GameEventListener_directions listener)
    { listeners.Remove(listener); }
}