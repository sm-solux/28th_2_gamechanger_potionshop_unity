using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potion : MonoBehaviour
{
    public static string fire; // 불의 강도
    public static string time; // 시간
    public static string potiongrade;   // 물약 등급
    public static string potionname;    // 물약 이름
    public static int potionnum = 0;   // 물약 점수
    public Image ploadingbar;    // 제조 완료 시 넘어가는 물약
    float currentValue; // 로딩바와 관련
    public float speed; // 로딩바와 관련
    bool isFilling = false; // 로딩바와 관련
    public List<String> realherb;   // 진짜 약초
    public List<String> getherb;   // 해금된 약초 리스트
    public List<String> ingredients;    // 손님이 요구한 조미료 리스트
    public List<String> potionrecipe;    // 물약 레시피 
    public static List<int> plist = new List<int>(); // 해금된 물약 리스트

    public Canvas customer, making; // 손님오는 캔버스, 만드는 캔버스
    public static bool makeover = false;    // 제조버튼 누르고 돌아왔을 때
    public AudioSource btnsound;    // 버튼 클릭 소리

    void Start()
    {
        btnsound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isFilling){
            currentValue += speed * Time.deltaTime;
            ploadingbar.fillAmount = currentValue / 100;

            if (currentValue >= 100f)
            {
                isFilling = false;
                Invoke("Move", 2f);
            }
        }
    }

    public void End(){  // 제조 버튼 클릭
        btnsound.Play();
        int num = customerManage.customerNum + 1;
        gradenum(num);
        Debug.Log(potionnum);
        Debug.Log(Getpotiongrade().ToString());
        Debug.Log(potionname);
        currentValue = 0f;
        ploadingbar.fillAmount = 0f;
    }

    void Move(){
        making.gameObject.SetActive(false);
        customer.gameObject.SetActive(true);
        Dispenser.loadingbar2.fillAmount = 0f;
        makeover = true;
        ploadingbar.fillAmount = 0f;
    }

    public void gradenum(int cusnum){ // 손님 번호로 엑셀 파일 읽고 내용과 일치하는지 확인하여 점수 매기기
        List<Dictionary<string, object>> data = CSVReader.Read("customerOrder");    // 손님번호-1 해야 알맞은 라인 읽어옴!

        if(data[cusnum-1]["fire"].ToString() == fire){    // 화력을 맞게 설정했는가
            potionnum += 1;
        }
        
        if(data[cusnum-1]["time"].ToString() == time){   // 시간을 맞게 설정했는가
            potionnum += 1;
        }
        
        ingredients = new List<string>();   // 리스트 초기화
        ingredlist(data, cusnum);   // 손님이 원한 조미료 리스트 가져오기

        for(int j = 0; j < Dragp.putgreds.Count; j++){
            Debug.Log(Dragp.putgreds[j]);
        }

        if((Dragp.putgreds != null) && (Dragp.putgreds.Count == ingredients.Count)){    // 내가 넣은 조미료의 개수와 손님이 원한 조미료의 개수가 같을 때
            for (int i = 0; i < Dragp.putgreds.Count; i++){
                if(ingredients.Contains(Dragp.putgreds[i])){    // 내가 넣은 조미료가 손님이 원한 조미료 리스트에 있을 때
                    if(i == Dragp.putgreds.Count - 1){
                        potionnum += 1;
                        break;
                    }
                }
                else{
                    break;
                }
            }
        }

        realherb = sellerManage.getTrueContractHerbList();  // 진짜 허브리스트
        
        if(Dragp.putherb != null){      // 찐약초인지 판별
            for (int i = 0; i < realherb.Count; i++){
                if(Dragp.putherb.Contains(realherb[i])){
                    if(i == realherb.Count - 1){
                        potionnum += 1;
                        break;
                    }
                }
                else{
                    break;
                }
            }    
        }

        potionstr(data, cusnum);    // 유저가 만든 물약이름 찾기

        string grade = Getpotiongrade();
        if(grade == "A"){
            Info();   // 만든 물약 리스트 제작(물약 노트)
        }

        potionrecipe.Clear();

        isFilling = true;   // 제조(로딩바)
    }

    void potionstr(List<Dictionary<string, object>> data, int cusnum){
        List<Dictionary<string, object>> potioninfo = CSVReader.Read("potioninfo");     // 물약 레시피 
        for (int i = 0; i < 18; i++){
            if(i == 17){    // 레시피에 없다면
                    potionnum = 0;
                    return;
            }

            potionrecipe = new List<string>();
            recipe(potioninfo, i);  // 물약 레시피 재료 리스트
            if(potionrecipe.Count == Dragp.specialherb.Count){  // 개수 같음
                for (int j = 0; j < potionrecipe.Count; j++){
                    if(potionrecipe.Contains(Dragp.specialherb[j])){    // 본 레시피에 있는 재료들인지
                        if(j == potionrecipe.Count - 1){    
                            if(potioninfo[i]["name"].ToString() == data[cusnum-1]["name"].ToString()){  // 내가 만든 물약과 손님이 원한 물약이 같다면
                                potionname = potioninfo[i]["name"].ToString();
                                return;
                            }
                            else{   // 내가 만든 물약과 손님이 원한 물약이 다르다면
                                if (potionnum < 2){ // 등급 f로 바로 하락
                                    potionnum = 0;
                                    return;
                                }
                                else{   // 등급 두 단계 하락
                                    potionnum -= 2;
                                    if(potionnum == 2){ // 두 단계 하락해도 C등급일 때
                                        potionname = potioninfo[i]["name"].ToString();
                                    }
                                    return;
                                }
                            }
                        }
                    }
                    else{   // 본 레시피에 없는 재료를 넣었다면
                        break;
                    }                    
                }
                continue;   // 다음 i번째줄 실행
            }
            else{   // 다음 i번째줄 실행
                continue;
            }
        }
    }

    public List<String> recipe(List<Dictionary<string, object>> potioninfo, int num){   // 물약 레시피 리스트
        if(potioninfo[num]["ingred1"] != null && !string.IsNullOrEmpty(potioninfo[num]["ingred1"].ToString())){
            potionrecipe.Add(potioninfo[num]["ingred1"].ToString());
            if(potioninfo[num]["ingred2"] != null && !string.IsNullOrEmpty(potioninfo[num]["ingred2"].ToString())){
                potionrecipe.Add(potioninfo[num]["ingred2"].ToString());
                if(potioninfo[num]["ingred3"] != null && !string.IsNullOrEmpty(potioninfo[num]["ingred3"].ToString())){
                    potionrecipe.Add(potioninfo[num]["ingred3"].ToString());
                    if(potioninfo[num]["ingred4"] != null && !string.IsNullOrEmpty(potioninfo[num]["ingred4"].ToString())){
                        potionrecipe.Add(potioninfo[num]["ingred4"].ToString());
                    }
                }
            }
        }

        return potionrecipe;
    }

    public List<String> ingredlist(List<Dictionary<string, object>> data, int cusnum){
        if(data[cusnum-1]["ingredient1"] != null && !string.IsNullOrEmpty(data[cusnum-1]["ingredient1"].ToString())){  // 손님이 원한 조미료 리스트
            ingredients.Add(data[cusnum-1]["ingredient1"].ToString());
            if(data[cusnum-1]["ingredient2"] != null && !string.IsNullOrEmpty(data[cusnum-1]["ingredient2"].ToString())){
                ingredients.Add(data[cusnum-1]["ingredient2"].ToString());
                if(data[cusnum-1]["ingredient3"] != null && !string.IsNullOrEmpty(data[cusnum-1]["ingredient3"].ToString())){
                    ingredients.Add(data[cusnum-1]["ingredient3"].ToString());
                    if(data[cusnum-1]["ingredient4"] != null && !string.IsNullOrEmpty(data[cusnum-1]["ingredient4"].ToString())){
                        ingredients.Add(data[cusnum-1]["ingredient4"].ToString());
                    }
                }
            }
        }
        return ingredients;
    }

    public static String Getpotiongrade(){ // 물약 등급 얻는 함수
        if(potionnum == 0){
            potiongrade = "F";
        }
        else if(potionnum == 1){
            potiongrade = "D";
        }
        else if(potionnum == 2){
            potiongrade = "C";
        }
        else if(potionnum == 3){
            potiongrade = "B";
        }
        else if(potionnum == 4){
            potiongrade = "A";
        }
        return potiongrade;
    }
    
    void Info(){    // 해금된 물약도감
        List<Dictionary<string, object>> potionimg = CSVReader.Read("potioninfo");
        for (int i = 0; i < 17; i++){
            if(potionname == potionimg[i]["name"].ToString()){
                    plist.Add(i);   // 게임 오브젝트 배열은 1~17
            }
        }
    }
    public void Remove(){
        btnsound.Play();
        Dragp.putgreds.Clear(); // 넣은 모든 재료들 초기화
        Dragp.putherb.Clear();  // 허브들 초기화
        Dragp.specialherb.Clear();  // 허브들과 부재료 리스트 초기화
        potionnum = 0;
        potionname = null;
    }
}
