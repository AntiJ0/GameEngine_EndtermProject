using UnityEngine;
using TMPro;

public class ShopUI : MonoBehaviour
{
    [Header("Upgrade Text References")]
    public TextMeshProUGUI healthUpgradeText;
    public TextMeshProUGUI goldUpgradeText;
    public TextMeshProUGUI shieldUpgradeText;

    private void OnEnable()
    {
        UpdateTexts();
    }

    public void OnUpgradeHealth()
    {
        UpgradeManager.Instance.UpgradeHealth();
        UpdateTexts();
    }

    public void OnUpgradeGoldBoost()
    {
        UpgradeManager.Instance.UpgradeGoldBoost();
        UpdateTexts();
    }

    public void OnUpgradeShield()
    {
        UpgradeManager.Instance.UpgradeShield();
        UpdateTexts();
    }

    private void UpdateTexts()
    {
        var mgr = UpgradeManager.Instance;

        if (healthUpgradeText != null)
            healthUpgradeText.text = $"{mgr.healthLevel} / {mgr.maxHealthLevel}";

        if (goldUpgradeText != null)
            goldUpgradeText.text = $"{mgr.goldBoostLevel} / {mgr.maxGoldBoostLevel}";

        if (shieldUpgradeText != null)
            shieldUpgradeText.text = $"{mgr.shieldLevel} / {mgr.maxShieldLevel}";
    }
}