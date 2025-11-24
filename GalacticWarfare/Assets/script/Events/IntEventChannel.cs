using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/IntEventChannel")]
public class IntEventChannel : ScriptableObject
{
    public UnityAction<int> OnEventRaised;
    public void Raise(int value) => OnEventRaised?.Invoke(value);
    public void Register(UnityAction<int> callback) => OnEventRaised += callback;
    public void Unregister(UnityAction<int> callback) => OnEventRaised -= callback;
}
