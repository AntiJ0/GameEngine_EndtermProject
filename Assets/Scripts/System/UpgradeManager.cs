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

    [Header("UI - 알림")]
    public TextMeshProUGUI notEnoughGoldText;

    private void Awake()
    {
        PlayerPrefs.DeleteAll(); 
        PlayerPrefs.Save();

        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        LoadUpgrades();

        if (notEnoughGoldText != null)
            notEnoughGoldText.gameObject.SetActive(false);
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
        else
        {
            ShowNotEnoughGold();
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
        else
        {
            ShowNotEnoughGold();
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
        else
        {
            ShowNotEnoughGold();
        }
    }

    private void UpdateUpgradeCostUI()
    {
        if (healthCostText != null)
        {
            if (healthLevel < maxHealthLevel)
                healthCostText.text = $"{healthUpgradeCosts[healthLevel]}골드";
            else
                healthCostText.text = "Max";
        }

        if (goldBoostCostText != null)
        {
            if (goldBoostLevel < maxGoldBoostLevel)
                goldBoostCostText.text = $"{goldBoostUpgradeCosts[goldBoostLevel]}골드";
            else
                goldBoostCostText.text = "Max";
        }

        if (shieldCostText != null)
        {
            if (shieldLevel < maxShieldLevel)
                shieldCostText.text = $"{shieldUpgradeCosts[shieldLevel]}골드";
            else
                shieldCostText.text = "Max";
        }
    }

    private void ShowNotEnoughGold()
    {
        if (notEnoughGoldText == null) return;

        notEnoughGoldText.gameObject.SetActive(false);
        StopAllCoroutines(); // 중복 방지
        StartCoroutine(ShowTextTemporarily());
    }

    private IEnumerator ShowTextTemporarily()
    {
        notEnoughGoldText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        notEnoughGoldText.gameObject.SetActive(false);
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

    // 필요 시 현재 골드 배율 확인 함수
    public float GetCurrentGoldBoostMultiplier()
    {
        return 1f + goldBoostLevel * 0.1f;
    }
}