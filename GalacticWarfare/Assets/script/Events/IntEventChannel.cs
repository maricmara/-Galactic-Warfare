using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Events/Int Event Channel")]
public class IntEventChannel : ScriptableObject
{
    public Action<int> OnEventRaised;

    public void Raise(int value)
    {
        OnEventRaised?.Invoke(value);
    }

    public void Register(Action<int> callback)
    {
        OnEventRaised += callback;
    }

    public void Unregister(Action<int> callback)
    {
        OnEventRaised -= callback;
    }
}
