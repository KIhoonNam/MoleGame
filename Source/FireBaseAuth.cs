using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Firebase.Auth;

public class FireBaseAuth : MonoBehaviour
{
    private FirebaseAuth auth;

    public static FireBaseAuth instance;

    FireBaseDataScript data;
    
    string username;
    string useremail;

    void Awake()
    {
        instance = this;

        PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder()
            .RequestIdToken()
            .RequestEmail()
            .Build());
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        // 구글 플레이 게임 활성화
        data = FindObjectOfType<FireBaseDataScript>();
        auth = FirebaseAuth.DefaultInstance; // Firebase 액세스
        TryGoogleLogin();
    }

    public void TryGoogleLogin()
    {
        if (!Social.localUser.authenticated) // 로그인 되어 있지 않다면
        {
            Social.localUser.Authenticate(success => // 로그인 시도
            {
                if (success) // 성공하면
                {
                    Debug.Log("Success");
                   StartCoroutine( TryFirebaseLogin()); // Firebase Login 시도
                }
                else // 실패하면
                {
                    Debug.Log("Fail");
                }
            });
        }
    }
    IEnumerator TryFirebaseLogin()
    {
        while (string.IsNullOrEmpty(((PlayGamesLocalUser)Social.localUser).GetIdToken()))
            yield return null;
        string idToken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();
        

        Credential credential = GoogleAuthProvider.GetCredential(idToken, null);
        auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }
            FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: ({1})",
                newUser.DisplayName, newUser.UserId);
            useremail = newUser.Email.ToString();
            username = newUser.UserId.ToString();
            data.SetUserId(username, useremail);
            data.Load();

            Debug.Log("Success!");
        });
    }
    public void OnShowLeaderBoard(int Score)
    {
  
        // 1000점을 등록
        Social.ReportScore(Score, GPGSIds.leaderboard_maxcombo, (bool bSuccess) =>
        {
            if (bSuccess)
            {
                Debug.Log("ReportLeaderBoard Success");

            }
            else
            {
                Debug.Log("ReportLeaderBoard Fall");

            }
        }
        );
      
    }
    public void OnAddAchievment(string name)
    {

        Social.ReportProgress(name, 100f, (bool bSucces) =>
        {
            if (bSucces)
            {
                Debug.Log("AddAchievement Success");

            }
            else
            {
                Debug.Log("AddAchievement Fall");

            }
        });
    }
    public void ShowLeaderboardUI()
    {
        Social.ShowLeaderboardUI();
    }
    public void OnShowAchievement()
    {

        Social.ShowAchievementsUI();
    }

}