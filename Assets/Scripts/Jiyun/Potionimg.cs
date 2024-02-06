using UnityEngine;
using UnityEngine.EventSystems;

public class Potionimg : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Vector2 bottlePos, capPos;
    public GameObject bottle, cap, finalbottle, afbottle;  // 병&뚜껑
    public GameObject[] completepotions;    // 완성된 물약들
    public string cpotion, grade;  // 물약 이름

    void Start()
    {
        foreach(GameObject apotion in completepotions){
            apotion.SetActive(false);
        }
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)  // 드래그 시작
    {
        
    }
    void IDragHandler.OnDrag(PointerEventData eventData)    // 드래그 중
    {
        // 스크린 좌표를 캔버스 좌표로 변환
        // eventData.position은 출력하고 싶은 스크린 좌표
        // Camera.main은 스크린 좌표와 연관된 카메라
        // localPos는 변환된 좌표를 저장한 변수
        if(gameObject.name == "bottle" && eventData.position.y > 370){
            eventData.position = new Vector2(eventData.position.x, 370f);
        }
        if(gameObject.name == "cap" && eventData.position.y > 500){
            eventData.position = new Vector2(eventData.position.x, 500f);
        }
        if(gameObject.name == "after" && eventData.position.y > 370){
            eventData.position = new Vector2(eventData.position.x, 370f);
        }
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform.parent, eventData.position, Camera.main, out Vector2 localPos);
        transform.localPosition = localPos;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData) // 드래그 끝
    {
        if(gameObject.name == "bottle"){    // 빈 물병
            Dispenser.isIn = true;
            Invoke("put", .1f);
        }
        if(gameObject.name == "after"){ // 디스펜서에서 나온 물병
            Invoke("put2", .1f);
        }
    }

    void put(){ // 위치고정
        this.transform.position = new Vector3(3.3f, -3.3f, 0f);
    }

    void put2(){
        this.transform.position = new Vector3(-0.5f, -3.1f, 0f);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "change" && gameObject.name == "cap"){
            afbottle.SetActive(false);    // 디스팬서에서 나온 물병
            cap.SetActive(false);   // 뚜껑
            complete();
        }   

        if(other.gameObject.name == "Trashcan"){
            gameObject.SetActive(false);
            gameObject.transform.position = new Vector3(0f, -3f, 0f);
            bottle.transform.position = new Vector3(0f, -6.4f, 0f); // 빈 물병
            cap.transform.position = new Vector3(3.1f, -6.5f, 0f);  // 뚜껑
            afbottle.transform.position = new Vector3(3.5f, -3f, 0f);
            bottle.SetActive(true);
            cap.SetActive(true);
            reload();
        }

        if(other.gameObject.name == "customerimg"){
            customerManage.crush = true;
            gameObject.SetActive(false);
            gameObject.transform.position = new Vector3(0f, -3f, 0f);
            bottle.transform.position = new Vector3(0f, -6.4f, 0f); // 빈 물병
            cap.transform.position = new Vector3(3.1f, -6.5f, 0f);  // 뚜껑
            afbottle.transform.position = new Vector3(3.5f, -3f, 0f);
            bottle.SetActive(true);
            cap.SetActive(true);
            reload();
        }
    }

    void reload(){
        Dispenser.isDone = false;
        Dispenser.isDone2 = false;
        Dispenser.isIn = false;
    }

    public void complete(){
        cpotion = Potion.potionname;
        grade = Potion.Getpotiongrade();

        if(grade == "F"){
            completepotions[52].SetActive(true);
            return;
        }
        if(grade == "D"){
            completepotions[51].SetActive(true);
            return;
        }

        if(cpotion == "즉사물약"){
            if(grade == "C"){
                completepotions[50].SetActive(true);
            }
            if(grade == "B"){
                completepotions[49].SetActive(true);
            }
            if(grade == "A"){
                completepotions[48].SetActive(true);
            }
        }

        if(cpotion == "캄캄물약"){
            if(grade == "C"){
                completepotions[47].SetActive(true);
            }
            if(grade == "B"){
                completepotions[46].SetActive(true);
            }
            if(grade == "A"){
                completepotions[45].SetActive(true);
            }
        }

        if(cpotion == "꽁꽁물약"){
            if(grade == "C"){
                completepotions[44].SetActive(true);
            }
            if(grade == "B"){
                completepotions[43].SetActive(true);
            }
            if(grade == "A"){
                completepotions[42].SetActive(true);
            }
        }

        if(cpotion == "시원물약"){
            if(grade == "C"){
                completepotions[41].SetActive(true);
            }
            if(grade == "B"){
                completepotions[40].SetActive(true);
            }
            if(grade == "A"){
                completepotions[39].SetActive(true);
            }
        }

        if(cpotion == "무효화물약"){
            if(grade == "C"){
                completepotions[38].SetActive(true);
            }
            if(grade == "B"){
                completepotions[37].SetActive(true);
            }
            if(grade == "A"){
                completepotions[36].SetActive(true);
            }
        }

        if(cpotion == "투명물약"){
            if(grade == "C"){
                completepotions[35].SetActive(true);
            }
            if(grade == "B"){
                completepotions[34].SetActive(true);
            }
            if(grade == "A"){
                completepotions[33].SetActive(true);
            }
        }

        if(cpotion == "환영물약"){
            if(grade == "C"){
                completepotions[32].SetActive(true);
            }
            if(grade == "B"){
                completepotions[31].SetActive(true);
            }
            if(grade == "A"){
                completepotions[30].SetActive(true);
            }
        }

        if(cpotion == "꿈물약"){
            if(grade == "C"){
                completepotions[29].SetActive(true);
            }
            if(grade == "B"){
                completepotions[28].SetActive(true);
            }
            if(grade == "A"){
                completepotions[27].SetActive(true);
            }
        }

        if(cpotion == "기절물약"){
            if(grade == "C"){
                completepotions[26].SetActive(true);
            }
            if(grade == "B"){
                completepotions[25].SetActive(true);
            }
            if(grade == "A"){
                completepotions[24].SetActive(true);
            }
        }

        if(cpotion == "사향물약"){
            if(grade == "C"){
                completepotions[23].SetActive(true);
            }
            if(grade == "B"){
                completepotions[22].SetActive(true);
            }
            if(grade == "A"){
                completepotions[21].SetActive(true);
            }
        }

        if(cpotion == "마비물약"){
            if(grade == "C"){
                completepotions[20].SetActive(true);
            }
            if(grade == "B"){
                completepotions[19].SetActive(true);
            }
            if(grade == "A"){
                completepotions[18].SetActive(true);
            }
        }

        if(cpotion == "활활물약"){
            if(grade == "C"){
                completepotions[17].SetActive(true);
            }
            if(grade == "B"){
                completepotions[16].SetActive(true);
            }
            if(grade == "A"){
                completepotions[15].SetActive(true);
            }
        }

        if(cpotion == "따끈물약"){
            if(grade == "C"){
                completepotions[14].SetActive(true);
            }
            if(grade == "B"){
                completepotions[13].SetActive(true);
            }
            if(grade == "A"){
                completepotions[12].SetActive(true);
            }
        }

        if(cpotion == "야광물약"){
            if(grade == "C"){
                completepotions[11].SetActive(true);
            }
            if(grade == "B"){
                completepotions[10].SetActive(true);
            }
            if(grade == "A"){
                completepotions[9].SetActive(true);
            }
        }

        if(cpotion == "무적물약"){
            if(grade == "C"){
                completepotions[8].SetActive(true);
            }
            if(grade == "B"){
                completepotions[7].SetActive(true);
            }
            if(grade == "A"){
                completepotions[6].SetActive(true);
            }
        }

        if(cpotion == "치유물약"){
            if(grade == "C"){
                completepotions[5].SetActive(true);
            }
            if(grade == "B"){
                completepotions[4].SetActive(true);
            }
            if(grade == "A"){
                completepotions[3].SetActive(true);
            }
        }

        if(cpotion == "폭발물약"){
            if(grade == "C"){
                completepotions[2].SetActive(true);
            }
            if(grade == "B"){
                completepotions[1].SetActive(true);
            }
            if(grade == "A"){
                completepotions[0].SetActive(true);
            }
        }
    }
}
