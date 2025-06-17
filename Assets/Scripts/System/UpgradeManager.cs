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
    public int maxShieldLevel = 2;
    public int[] shieldUpgradeCosts = new int[2];

    [Header("UI")]
    public TextMeshProUGUI notEnoughGoldText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (notEnoughGoldText != null)
            notEnoughGoldText.gameObject.SetActive(false);
    }

    public void UpgradeHealth()
    {
        if (healthLevel >= maxHealthLevel) return;

        int cost = healthUpgradeCosts[healthLevel];
        if (GoldManager.Instance.TrySpendGold(cost))
        {
            healthLevel++;
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
        }
        else
        {
            ShowNotEnoughGold();
        }
    }

    private void ShowNotEnoughGold()
    {
        if (notEnoughGoldText == null) return;
        StopAllCoroutines(); // 중복 방지
        StartCoroutine(ShowTextTemporarily());
    }

    private IEnumerator ShowTextTemporarily()
    {
        notEnoughGoldText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        notEnoughGoldText.gameObject.SetActive(false);
    }
}