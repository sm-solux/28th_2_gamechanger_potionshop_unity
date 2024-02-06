using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image bottleImage; // Bottle �̹���
    public Image oneImage; // One �̹���
    private Image capImage; // Cap �̹���
    public class PotionIngredient
    {
        public string herb; // ����
        public string ingredient; // �����
    }

    private void Awake()
    {
        capImage = GetComponent<Image>(); // Cap �̹����� Image ������Ʈ�� �����ɴϴ�.
        oneImage.enabled = false; // One �̹����� ������ �ʰ� �����մϴ�.
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
            capImage.enabled = false; // Cap �̹����� ������ �ʰ� �����մϴ�.
            bottleImage.enabled = false; // Bottle �̹����� ������ �ʰ� �����մϴ�.

            oneImage.transform.position = bottleImage.transform.position; // One �̹����� ��ġ�� Bottle �̹����� ��ġ�� �����մϴ�.
            oneImage.enabled = true; // One �̹����� ���̰� �����մϴ�.
        }
    }
}
