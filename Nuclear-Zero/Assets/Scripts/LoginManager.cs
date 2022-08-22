using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class LoginManager : MonoBehaviour
{
    [SerializeField] Text _status;
    private void Awake()
    {
        //구성 및 초기화
        PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder().Build());
        //디버깅에 권장됨
        PlayGamesPlatform.DebugLogEnabled = true;
        //PlayGamesPlatform 활성화
        PlayGamesPlatform.Activate();
    }

    public void PlayLogin()
    {
        //현재 사용자가 인증되었는지 확인합니다
        if (!Social.localUser.authenticated)
        {
            //현재 활성 Social API 구현에 대한 로컬 사용자를 인증하고 그의 프로필 데이터를 가져옵니다
            //첫번째 인자 : 성공여부 / 두번째 인자 : 실패시 오류 로그
            Social.localUser.Authenticate((bool isOk, string error) =>
            {
                if (isOk)
                    _status.text = Social.localUser.userName;
                else
                    _status.text = error;
            });
        }
    }

    public void PlayLogout()
    {
        //Social.Active : 현재 활성화된 소셜 플랫폼(지금 상황에서는 PlayGamesPlatform)을 반환
        PlayGamesPlatform platform = Social.Active as PlayGamesPlatform;
        platform.SignOut();
        _status.text = "Logout";
    }
}
