using UnityEngine;
using UnityEngine.EventSystems;

public class DragSell : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    private Vector2 originalPosition;
    public RectTransform deskTransform; // Desk �г��� RectTransform
    private RectTransform selfTransform; // �ڽ��� RectTransform

    private void Awake()
    {
        // Desk �г��� ã�Ƽ� ���� RectTransform�� �����ɴϴ�.
        // ���� DragSell ��ũ��Ʈ�� Desk �г��� �ڽ� ������Ʈ�� �ִٸ�, �� �ڵ�� �ʿ� �����ϴ�.
        deskTransform = GameObject.Find("Bot").GetComponent<RectTransform>();
        selfTransform = GetComponent<RectTransform>(); // �ڽ��� RectTransform�� �����ɴϴ�.
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(deskTransform, eventData.position, null, out localPoint))
        {
            Vector3 newLocalPosition = deskTransform.localPosition;

            float clampedX = Mathf.Clamp(localPoint.x, deskTransform.rect.min.x + selfTransform.rect.width / 2, deskTransform.rect.max.x - selfTransform.rect.width / 2);
            float clampedY = Mathf.Clamp(localPoint.y, deskTransform.rect.min.y + selfTransform.rect.height / 2, deskTransform.rect.max.y - selfTransform.rect.height / 2);

            newLocalPosition.x = clampedX;
            newLocalPosition.y = clampedY;

            transform.position = deskTransform.TransformPoint(newLocalPosition);
        }
        else
        {
            transform.position = originalPosition;
        }
    }
}
