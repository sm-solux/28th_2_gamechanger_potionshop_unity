using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableBottle : MonoBehaviour, IEndDragHandler
{
    public ImageChanger imageChanger; // ImageChanger ��ũ��Ʈ�� �����ϴ� ����

    public void OnEndDrag(PointerEventData eventData)
    {
        imageChanger.InsertBottle(); // �巡�װ� ������ InsertBottle �Լ��� ȣ��
    }
}

