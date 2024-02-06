using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class Save : MonoBehaviour
{
    public TextMeshProUGUI txtDate;
    public TextMeshProUGUI txtSaveResult;

    // Start is called before the first frame update
    void Start()
    {
        int date = customerManage.date - 1;
        txtDate.text = "DAY " + date;
        txtSaveResult.text = "";
        Invoke("Nextday", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Nextday()
    {
        txtDate.text = "DAY " + customerManage.date;
        txtSaveResult.text = "저장 중 ...";
        Saving();
    }

    public void Saving()        // 날짜, 소지금, 물약 도감, 호감도, 플래그(분기점)
    {
        // 날짜, 소지금 저장
        PlayerPrefs.SetInt("SavedDate", customerManage.date);
        PlayerPrefs.SetInt("SavedMoney", customerManage.money);

        // 물약 도감 저장
        if(Potion.plist.Count != 0)
        {
            List<int> potionList = new List<int>();
            potionList = Potion.plist.ToList();         // 해금된 물약 도감 리스트 복제
            string str = "";
            for(int i = 0; i < Potion.plist.Count; i++)
            {
                str = str + potionList[i];
                if(i < 16)
                {
                    str = str + ",";
                }
            }
            PlayerPrefs.SetString("SavedPotionList", str);      // 배열을 ','으로 이어 문자열로 저장
        }
        
        // 호감도 저장
        PlayerPrefs.SetInt("SavedGLove", sellerManage.govermentLove);
        PlayerPrefs.SetInt("SavedRLove", sellerManage.rebelLove);

        // 플래그 저장
        PlayerPrefs.SetInt("SavedFlag1", System.Convert.ToInt16(sellerManage.rebelEvent));
        PlayerPrefs.SetInt("SavedFlag2", System.Convert.ToInt16(sellerManage.rebelEvent2));

        Invoke("Saved", 1.0f);
    }

    void Saved()
    {
        txtSaveResult.text = "저장 완료!";
        Invoke("Next", 0.5f);
    }

    void Next()
    {
        SceneManager.LoadScene("morningScene");
    }
}
