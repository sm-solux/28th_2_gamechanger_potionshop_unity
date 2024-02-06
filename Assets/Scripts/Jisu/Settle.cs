using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SettleScript : MonoBehaviour
{
    public TextMeshProUGUI txtDate;
    public TextMeshProUGUI txtNowMoney;
    public TextMeshProUGUI txtInterest;
    public TextMeshProUGUI txtRent;
    public TextMeshProUGUI txtFinalMoney;

    int date = customerManage.getDate();
    int nowMoney = customerManage.getMoney();
    int interest = 300;
    int rent = 500;

    // Start is called before the first frame update
    void Start()
    {
        txtDate.text = "DAY " + date;
        txtNowMoney.text = nowMoney + " 원";
        int plus = (date/5) * 200;
        rent += plus;
        Invoke("SettleMoney", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SettleMoney()
    {
        txtInterest.text = interest + " 원";
        txtRent.text = rent + " 원";
        nowMoney -= (interest + rent);
        Invoke("FinalMoney", 0.5f);
    }

    void FinalMoney()
    {
        txtFinalMoney.text = nowMoney + " 원";      
    }

    public void NextScene()
    {
        customerManage.money = nowMoney;

        if(customerManage.money >= 0)        
        {
            customerManage.dateIncrese();
            SceneManager.LoadScene("SaveScene");
        }
        else        // 파산 엔딩
        {
            SceneManager.LoadScene("BankruptcyEnding");
        }
    }
}
