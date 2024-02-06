using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableBottle : MonoBehaviour, IEndDragHandler
{
    public ImageChanger imageChanger; // ImageChanger 스크립트를 참조하는 변수

    public void OnEndDrag(PointerEventData eventData)
    {
        imageChanger.InsertBottle(); // 드래그가 끝나면 InsertBottle 함수를 호출
    }
}

