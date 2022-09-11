using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames.BasicApi.SavedGame;
using System.Text;

public class TitleScene : BaseScene
{
    
    string log;
    protected override void Init()
    {
        base.Init();
        SaveAndLogin();
        //DataManager.Instance.LoadPlayerInfo();
        GameAudioManager.Instance.PlayBackGround("LobbyBGM");
        UIManager.Instance.ShowSceneUi<TitleUI>();

    }

    private void SaveAndLogin()
    {
        if(GPGSManager.Instance.OnSavedGameDataReadComplete != null)
        {
            GPGSManager.Instance.OnSavedGameDataReadComplete = (status, bytes) =>
            {
                if (status == SavedGameRequestStatus.Success)
                {
                    string strCloudData = null;
                    if (bytes.Length == 0)
                    {
                        strCloudData = string.Empty;
                        Debug.Log("로드 실패, 데이터 없음");
                    }
                    else
                    {
                        strCloudData = Encoding.UTF8.GetString(bytes);
                        DataManager.Instance.StringToGameInfo(strCloudData);
                    }
                }
                else
                {
                    
                }
            };
        }
        else
        {
            DataManager.Instance.LoadPlayerInfo();
        }
        
        GPGSManager.Instance.OnSavedGameDataWrttenComplete = (status, game) =>
        {
            if (status == SavedGameRequestStatus.Success)
            {
                Debug.Log("클라우드에 저장했습니다.");
            }
        };
        GPGSManager.Instance.SingIn((success) => log = $"{success}");
        //GPGSManager.Instance.LoadData();
    }

    public override void Clear()
    {
        //UIManager.Instance.Clear();
    }
}
