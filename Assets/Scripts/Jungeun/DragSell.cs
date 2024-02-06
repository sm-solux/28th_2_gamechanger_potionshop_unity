using UnityEngine;
using UnityEngine.EventSystems;

public class DragSell : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    private Vector2 originalPosition;
    public RectTransform deskTransform; // Desk 패널의 RectTransform
    private RectTransform selfTransform; // 자신의 RectTransform

    private void Awake()
    {
        // Desk 패널을 찾아서 그의 RectTransform를 가져옵니다.
        // 만약 DragSell 스크립트가 Desk 패널의 자식 오브젝트에 있다면, 이 코드는 필요 없습니다.
        deskTransform = GameObject.Find("Bot").GetComponent<RectTransform>();
        selfTransform = GetComponent<RectTransform>(); // 자신의 RectTransform를 가져옵니다.
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
