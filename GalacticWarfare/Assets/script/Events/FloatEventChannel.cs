using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/FloatEventChannel")]
public class FloatEventChannel : ScriptableObject
{
    public UnityAction<float> OnEventRaised;
    public void Raise(float value) => OnEventRaised?.Invoke(value);
    public void Register(UnityAction<float> callback) => OnEventRaised += callback;
    public void Unregister(UnityAction<float> callback) => OnEventRaised -= callback;
}
