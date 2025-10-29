using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Event Channels/Game Event")]
public class GameEvent : ScriptableObject
{
    private event Action<object> listeners;
    public void Raise(object data = null) => listeners?.Invoke(data);
    public void Register(Action<object> callback) => listeners += callback;
    public void Unregister(Action<object> callback) => listeners -= callback;
}