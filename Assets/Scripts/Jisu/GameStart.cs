using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;
using GooglePlayGames.BasicApi;
using System;

public class GameStart : MonoBehaviour
{
    public TextMeshProUGUI conStartResult;
    public GameObject title;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void NewStart()
    {
        PlayerPrefs.DeleteAll();
        customerManage.date = 1;
        customerManage.money = 1000;
        SceneManager.LoadScene("morningScene");
        // 튜토리얼 씬 만들면 그쪽으로 이동
    }

    public void ConStart()
    {
        // 저장한 데이터 가져오기
        if(PlayerPrefs.HasKey("SavedDate")) // Date로 저장된 값이 있다면
        {
            // 날짜 가져오기
            customerManage.date = PlayerPrefs.GetInt("SavedDate");

            // 소지금 가져오기
            customerManage.money = PlayerPrefs.GetInt("SavedMoney");

            // 물약 해금 상황 가져오기
            if(PlayerPrefs.HasKey("SavedPotionList"))
            {
                string[] dataArr = PlayerPrefs.GetString("SavedPotionList").Split(',');
                if (dataArr[0] != null)
                {
                    List<int> savedPotionList = new List<int>();
                    for(int i = 0; i < dataArr.Length; i++)
                    {
                        // 안전하게 변환하는 방법 사용
                        if (int.TryParse(dataArr[i], out int potionValue))
                        {
                            savedPotionList.Add(potionValue);
                        }
                        else
                        {
                            // 변환 실패 시 예외 처리 또는 기본값 사용
                            Debug.LogError("Invalid potion value: " + dataArr[i]);
                        }
                        // savedPotionList.Add(Convert.ToInt32(dataArr[i]));
                    }
                    Potion.plist = savedPotionList.ToList();
                }
                
            }

            // 호감도 가져오기
            sellerManage.govermentLove = PlayerPrefs.GetInt("SavedGLove");
            sellerManage.rebelLove = PlayerPrefs.GetInt("SavedRLove");

            // 플래그 가져오기
            sellerManage.rebelEvent = System.Convert.ToBoolean(PlayerPrefs.GetInt("SavedFlag1"));
            sellerManage.rebelEvent2 = System.Convert.ToBoolean(PlayerPrefs.GetInt("SavedFlag2"));

            // 불러온 정보로 아침 씬 로드하기
            SceneManager.LoadScene("morningScene");
        }
        else    // 저장된 기록이 없을 경우
        {
            conStartResult.text = "저장 기록이 없습니다.";
        }
    }
}
