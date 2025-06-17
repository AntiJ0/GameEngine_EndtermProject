using UnityEngine;
using TMPro;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance { get; private set; }

    public int currentGold = 0;
    public TextMeshProUGUI goldText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdateGoldUI();
    }

    public void AddGold(int amount)
    {
        currentGold += amount;
        UpdateGoldUI();
    }

    public bool TrySpendGold(int amount)
    {
        if (currentGold >= amount)
        {
            currentGold -= amount;
            UpdateGoldUI();
            return true;
        }
        return false;
    }

    private void UpdateGoldUI()
    {
        if (goldText != null)
        {
            goldText.text = currentGold.ToString();
        }
    }
}