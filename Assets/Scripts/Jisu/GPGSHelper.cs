using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// Google Play Games Service
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GPGSHelper : MonoBehaviour
{
    public TextMeshProUGUI txtLoginResult;

    // Start is called before the first frame update
    void Start()
    {
        var config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

   // 로그인 시도 -> 결과 보고
    public void Login()
   {
      PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (success) =>
      {         
         if (success == SignInStatus.Success) {
            txtLoginResult.text = "로그인 성공!";
            SceneManager.LoadScene("morningScene");
         } 
         else
            txtLoginResult.text = "로그인 실패...";
      });
   }
}