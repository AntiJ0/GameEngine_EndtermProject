using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    public GameObject targetPanel;
    public GameObject targetPanel2;

    public void Toggle()
    {
        if (targetPanel != null)
        {
            targetPanel.SetActive(!targetPanel.activeSelf);
        }
    }

    public void Toggle2()
    {
        if (targetPanel2 != null)
        {
            bool newState = !targetPanel2.activeSelf;
            targetPanel2.SetActive(newState);

            if (newState) GoldManager.Instance.RefreshGoldUI();
        }
    }
}