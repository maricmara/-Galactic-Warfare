using UnityEngine;
using UnityEngine.UI;

public class BossHUD : MonoBehaviour
{
    public IntEventChannel bossHpEvent;
    public Image bossHpBar;

    private int maxHp = 1000; // igual ao BossData, ou pode receber via evento

    void OnEnable()
    {
        if (bossHpEvent != null)
            bossHpEvent.Register(UpdateHPBar);
    }

    void OnDisable()
    {
        if (bossHpEvent != null)
            bossHpEvent.Unregister(UpdateHPBar);
    }

    void UpdateHPBar(int hp)
    {
        bossHpBar.fillAmount = (float)hp / maxHp;
    }
}
