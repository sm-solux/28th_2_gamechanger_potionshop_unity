using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class yuraDrag : MonoBehaviour,IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public static Vector2 DefaultPos;   // 처음 위치

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)  // 드래그 시작
    {
        DefaultPos = this.transform.position;
    }
    void IDragHandler.OnDrag(PointerEventData eventData)    // 드래그 중
    {
        // 스크린 좌표를 캔버스 좌표로 변환
        // eventData.position은 출력하고 싶은 스크린 좌표
        // Camera.main은 스크린 좌표와 연관된 카메라
        // localPos는 변환된 좌표를 저장한 변수
        
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform.parent, eventData.position, Camera.main, out Vector2 localPos);

        transform.localPosition = localPos;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData) // 드래그 끝
    {
        Invoke("Destroy", .5f);
        Debug.Log("cdfsfd"); 
    }

    void Destroy()
    { // 원위치로
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) // 충돌 발생
    {
        if (other.gameObject.tag.Equals("customertag"))
        {
            if (gameObject.tag.Equals("potiontag"))
            {
                Debug.Log("cdfsfd");
            }
        }
    }

}


