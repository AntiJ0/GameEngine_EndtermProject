using System.Collections;
using UnityEngine;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    [Header("Health Upgrade")]
    public int healthLevel = 0;
    public int maxHealthLevel = 10;
    public int[] healthUpgradeCosts = new int[10];

    [Header("Gold Boost Upgrade")]
    public int goldBoostLevel = 0;
    public int maxGoldBoostLevel = 10;
    public int[] goldBoostUpgradeCosts = new int[10];

    [Header("Shield Upgrade")]
    public int shieldLevel = 0;
    public int maxShieldLevel = 1;
    public int[] shieldUpgradeCosts = new int[1];

    [Header("UI - Cost Display")]
    public TextMeshProUGUI healthCostText;
    public TextMeshProUGUI goldBoostCostText;
    public TextMeshProUGUI shieldCostText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        LoadUpgrades();
    }

    private void Start()
    {
        UpdateUpgradeCostUI();
    }

    public void UpgradeHealth()
    {
        if (healthLevel >= maxHealthLevel) return;

        int cost = healthUpgradeCosts[healthLevel];
        if (GoldManager.Instance.TrySpendGold(cost))
        {
            healthLevel++;
            SaveUpgrades();
            UpdateUpgradeCostUI();
        }
    }

    public void UpgradeGoldBoost()
    {
        if (goldBoostLevel >= maxGoldBoostLevel) return;

        int cost = goldBoostUpgradeCosts[goldBoostLevel];
        if (GoldManager.Instance.TrySpendGold(cost))
        {
            goldBoostLevel++;
            SaveUpgrades();
            UpdateUpgradeCostUI();
        }
    }

    public void UpgradeShield()
    {
        if (shieldLevel >= maxShieldLevel) return;

        int cost = shieldUpgradeCosts[shieldLevel];
        if (GoldManager.Instance.TrySpendGold(cost))
        {
            shieldLevel++;
            SaveUpgrades();
            UpdateUpgradeCostUI();
        }
    }

    private void UpdateUpgradeCostUI()
    {
        if (healthCostText != null)
        {
            if (healthLevel < maxHealthLevel)
                healthCostText.text = $"{healthUpgradeCosts[healthLevel]}°ñµå";
            else
                healthCostText.text = "Max";
        }

        if (goldBoostCostText != null)
        {
            if (goldBoostLevel < maxGoldBoostLevel)
                goldBoostCostText.text = $"{goldBoostUpgradeCosts[goldBoostLevel]}°ñµå";
            else
                goldBoostCostText.text = "Max";
        }

        if (shieldCostText != null)
        {
            if (shieldLevel < maxShieldLevel)
                shieldCostText.text = $"{shieldUpgradeCosts[shieldLevel]}°ñµå";
            else
                shieldCostText.text = "Max";
        }
    }

    private void SaveUpgrades()
    {
        PlayerPrefs.SetInt("HealthLevel", healthLevel);
        PlayerPrefs.SetInt("GoldBoostLevel", goldBoostLevel);
        PlayerPrefs.SetInt("ShieldLevel", shieldLevel);
        PlayerPrefs.Save();
    }

    private void LoadUpgrades()
    {
        healthLevel = PlayerPrefs.GetInt("HealthLevel", 0);
        goldBoostLevel = PlayerPrefs.GetInt("GoldBoostLevel", 0);
        shieldLevel = PlayerPrefs.GetInt("ShieldLevel", 0);
    }

    // ÇÊ¿ä ½Ã ÇöÀç °ñµå ¹èÀ² È®ÀÎ ÇÔ¼ö
    public float GetCurrentGoldBoostMultiplier()
    {
        return 1f + goldBoostLevel * 0.1f;
    }
}