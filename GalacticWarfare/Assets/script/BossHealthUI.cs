using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    public IntEventChannel bossHPEvent;
    public Slider slider;

    private void OnEnable()
    {
        if (bossHPEvent != null)
            bossHPEvent.Register(UpdateBar);
    }

    private void OnDisable()
    {
        if (bossHPEvent != null)
            bossHPEvent.Unregister(UpdateBar);
    }

    private void Start()
    {
        if (slider != null)
            slider.value = slider.maxValue;
    }

    private void UpdateBar(int currentHP)
    {
        if (slider != null)
            slider.value = currentHP;
    }
}
