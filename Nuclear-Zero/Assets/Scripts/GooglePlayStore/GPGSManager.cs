using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using System.Text;
using Newtonsoft.Json;
using System.IO;

public class GPGSManager : Managers<GPGSManager>
{
    public Action<SavedGameRequestStatus, byte[]> OnSavedGameDataReadComplete;
    public Action<SavedGameRequestStatus, ISavedGameMetadata> OnSavedGameDataWrttenComplete;
    private bool isSaving;
    private const string FILE_NAME = "PlayerInfomation.bin";

    public override void Init()
    {
        PlayGamesClientConfiguration conf = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(conf);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    public void SingIn(Action<bool> onComplete)
    {
        Social.localUser.Authenticate(onComplete);
    }

    #region 저장

    public void SaveData()
    {
        if (Social.localUser.authenticated)
        {
            this.isSaving = true;
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            savedGameClient.OpenWithAutomaticConflictResolution(FILE_NAME, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, this.OnSavedGameOpened); ;
        }
        SaveLocal();
    }

    private void SaveLocal()
    {
        DataManager.Instance.SavePlayerInfo();
    }

    private void SaveGame(ISavedGameMetadata data)
    {
        this.SaveLocal();

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();
        var stringToSave = DataManager.Instance.GameInfoToString();
        byte[] bytes = Encoding.UTF8.GetBytes(stringToSave);
        savedGameClient.CommitUpdate(data, update, bytes, OnSavedGameDataWrttenComplete);
    }

    #endregion

    #region 불러오기
    public void LoadData()
    {
        if (Social.localUser.authenticated)
        {
            this.isSaving = false;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(FILE_NAME, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, this.OnSavedGameOpened);
        }
        else
        {
            this.LoadLocal();
        }
    }

    private void LoadLocal()
    {
        DataManager.Instance.LoadPlayerInfo();
    }

    private void LoadGame(ISavedGameMetadata data)
    {
        ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(data, OnSavedGameDataReadComplete);
        DataManager.Instance.IsLoadPlayerInfo = true;
    }

    #endregion
    private void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        Debug.LogFormat("OnSavedGameOpened : {0}, {1}", status, isSaving);
        if (status == SavedGameRequestStatus.Success)
        {
            if (!isSaving)
            {
                this.LoadGame(game);
            }
            else
            {
                this.SaveGame(game);
            }
        }
        else
        {
            if (!isSaving)
            {
                this.LoadLocal();
            }
            else
            {
                this.SaveLocal();
            }
        }
    }

    public void SetStageAchiv()
    {
        Social.ReportProgress(GPGSIds.achievement__44, 100.0f, (success) => Debug.Log("업적 달성"));
    }

    public void SetLogin()
    {
        Social.ReportProgress(GPGSIds.achievement, 100.0f, (success) => Debug.Log("업적 달성"));
    }
}
