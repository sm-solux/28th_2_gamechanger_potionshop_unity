using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image bottleImage; // Bottle 이미지
    public Image oneImage; // One 이미지
    private Image capImage; // Cap 이미지
    public class PotionIngredient
    {
        public string herb; // 약초
        public string ingredient; // 부재료
    }

    private void Awake()
    {
        capImage = GetComponent<Image>(); // Cap 이미지의 Image 컴포넌트를 가져옵니다.
        oneImage.enabled = false; // One 이미지를 보이지 않게 설정합니다.
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var topQuarterArea = new Rect(
            bottleImage.rectTransform.rect.x,
            bottleImage.rectTransform.rect.y + bottleImage.rectTransform.rect.height * 0.75f,
            bottleImage.rectTransform.rect.width,
            bottleImage.rectTransform.rect.height * 0.25f
        );

        if (RectTransformUtility.RectangleContainsScreenPoint(bottleImage.rectTransform, eventData.position) &&
            topQuarterArea.Contains(bottleImage.rectTransform.InverseTransformPoint(eventData.position)))
        {
            capImage.enabled = false; // Cap 이미지를 보이지 않게 설정합니다.
            bottleImage.enabled = false; // Bottle 이미지를 보이지 않게 설정합니다.

            oneImage.transform.position = bottleImage.transform.position; // One 이미지의 위치를 Bottle 이미지의 위치로 설정합니다.
            oneImage.enabled = true; // One 이미지를 보이게 설정합니다.
        }
    }
}
