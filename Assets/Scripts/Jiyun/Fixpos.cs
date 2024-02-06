using UnityEngine;
using UnityEngine.EventSystems;

public class Fixpos : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler 
{
    public Vector2 DefaultPos;
    public GameObject bottle, cap;  // 병&뚜껑

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
        if(eventData.position.y > 180){
            eventData.position = new Vector2(eventData.position.x, 180f);
        }
        if(gameObject.name == "cap" && eventData.position.y > 240){
            eventData.position = new Vector2(eventData.position.x, 240f);
        }
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform.parent, eventData.position, Camera.main, out Vector2 localPos);
        transform.localPosition = localPos;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData) // 드래그 끝
    {
        if(gameObject.name == "bottle"){
            Invoke("put", .1f);
        }
    }

    void put(){ // 위치고정
        this.transform.position = new Vector3(3.5f, -3f, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "change" && gameObject.name == "cap"){
            bottle.SetActive(false);
            cap.SetActive(false);
            //complete();   // 완성된 물약 띄우기
        }   
    }
}
