using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonColorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public Color normalColor = Color.white;
    public Color hoverColor = new Color(0.9f, 0.9f, 0.9f);
    public Color clickColor = new Color(0.7f, 0.7f, 0.7f);

    private Image buttonImage;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.color = normalColor;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonImage != null)
            buttonImage.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonImage != null)
            buttonImage.color = normalColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (buttonImage != null)
            buttonImage.color = clickColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 다시 hover 상태인지 여부를 판별해서 색상 처리
        if (buttonImage != null)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(
                transform as RectTransform,
                eventData.position,
                eventData.enterEventCamera))
            {
                buttonImage.color = hoverColor;
            }
            else
            {
                buttonImage.color = normalColor;
            }
        }
    }
}