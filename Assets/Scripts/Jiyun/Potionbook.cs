using UnityEngine;
using DG.Tweening;
using TMPro;

public class PotionBook : MonoBehaviour
{
    public RectTransform panel;
    public GameObject plock, prev, next;    // 잠금 이미지
    public GameObject[] potions;    // 해금된 물약 사진
    public int currentpage = 0;    // 현재 페이지
    public TextMeshProUGUI ptext;   // 페이지

    void Start() {
        ShowPage(currentpage);
    }
    public void Open(){ // 노트 펼치기
        panel.DOLocalMoveY(0, 1f).SetEase(Ease.OutBack);
    }
    public void Close(){    // 노트 닫기
        panel.DOLocalMoveY(-524, 1f).SetEase(Ease.InBack);
    }
    void ShowPage(int page){
        prev.SetActive(true);
        next.SetActive(true);
        plock.SetActive(false);
        
        foreach(GameObject potion in potions){
            potion.SetActive(false);
        }
        if(page == 0){
            prev.SetActive(false);
            if(Potion.plist.Count > 0){
                Debug.Log("치유물약!");
                potions[page].SetActive(true);
            }
        }
        if(page == potions.Length - 1){
            next.SetActive(false);
        }

        if(Potion.plist != null && Potion.plist.Contains(page)){
            potions[page].SetActive(true);   // 현재 페이지만 보여주기
        }
        else{   // 해금 안되었으면 잠금 화면
            plock.SetActive(true);
        }        
        ptext.text = (page+1).ToString();
    }
    public void Prev(){ // 이전 페이지
        currentpage--;
        
        if (currentpage >= 0){
            ShowPage(currentpage);
        }
        else{   // 맨 뒷장으로
            currentpage = potions.Length - 1;
            ShowPage(currentpage);
        }        
    }
    public void Next(){ // 다음 페이지
        currentpage++;

        if(currentpage < potions.Length){
            ShowPage(currentpage);
        }
        else{
            currentpage = 0;    // 다시 첫 장으로
            ShowPage(currentpage);
        }
    }
}
