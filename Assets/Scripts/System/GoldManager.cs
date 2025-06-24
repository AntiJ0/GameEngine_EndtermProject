using UnityEngine;
using TMPro;                      
using UnityEngine.SceneManagement;

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

        // 씬이 바뀔 때마다 goldText 다시 연결
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // 씬 변경 이벤트 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        TryFindGoldText();
        UpdateGoldUI();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        TryFindGoldText();
        UpdateGoldUI();
    }

    void TryFindGoldText()
    {
        if (goldText == null)
        {
            // 모든 TextMeshProUGUI를 찾아서 이름이 GoldText인 것 선택
            var allTexts = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>();
            foreach (var txt in allTexts)
            {
                if (txt.name == "GoldText")
                {
                    goldText = txt;
                    break;
                }
            }
        }
    }

    public void AddGold(int amount)
    {
        float boostMultiplier = 1f + UpgradeManager.Instance.goldBoostLevel * 0.1f;
        int finalGold = Mathf.RoundToInt(amount * boostMultiplier);
        currentGold += finalGold;
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

    public void RefreshGoldUI()
    {
        UpdateGoldUI();
    }
}