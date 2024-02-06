using UnityEngine;
using UnityEngine.UI;
using System.Collections; // 코루틴 사용을 위해 추가
using System.Collections.Generic;

public class ImageChanger : MonoBehaviour
{
    public GameObject addbottleliq, adddoorbtn, open, close, openbtn, closebtn, bottle, openimage;  // 닫힌 이미지, 열린 이미지
    private bool isBottleInserted = false; // bottle 이미지가 들어갔는지 확인하는 변수
    private float buttonPressTime = 0; // addliqbtn을 누른 시간

    void Start()
    {
        open.SetActive(false);  // 열린 이미지 안 보이게
        closebtn.SetActive(false);  // 닫는 버튼 안 보이게
    }

    public void ChangeImage()   // 열림 버튼 눌렀을 때
    {
        if (!isBottleInserted || (isBottleInserted && buttonPressTime >= 2.5f))
        {
            openbtn.SetActive(true);
            open.SetActive(true);
            closebtn.SetActive(true);
            close.SetActive(false);
        }
    }
    public void InsertBottle() // bottle 이미지가 들어갔을 때 호출
    {
        // 병 이미지의 위치가 (33f, -6f) ~ (41f, -17f) 범위 내에 있는지 확인
        if (bottle.transform.position.x > 33f && bottle.transform.position.x < 41f && bottle.transform.position.y > -17f && bottle.transform.position.y < -6f)
        {
            isBottleInserted = true;
            bottle.transform.position = new Vector3(37.5f, -11.5f); // bottle의 위치를 (37.5, -11.5)로 변경
            StartCoroutine(CountButtonPressTime());
        }
    }


    public void BackImage() // 닫힘 버튼 눌렀을 때
    {
        if (!isBottleInserted || (isBottleInserted && buttonPressTime >= 2.5f))
        {
            close.SetActive(true);
            open.SetActive(false);
            closebtn.SetActive(false);
        }
    }


    public void PressAddLiqBtn() // addliqbtn을 눌렀을 때 호출
    {
        buttonPressTime = 0;
    }

    IEnumerator CountButtonPressTime() // addliqbtn을 누른 시간을 계산하는 코루틴
    {
        while (buttonPressTime < 2.5f)
        {
            buttonPressTime += Time.deltaTime;
            yield return null;
        }
    }
}
