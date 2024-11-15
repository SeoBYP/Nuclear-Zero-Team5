using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public enum Scene
{
    Title,
    Lobby,
    Game,
}
class SceneManagerEx : Managers<SceneManagerEx>
{
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    public void LoadScene(Scene scene)
    {
        StartCoroutine(GameScene(scene));
    }

    public void ReLoadScene(Scene scene)
    {
        CurrentScene.Clear();
        SceneManager.LoadScene(GetSceneName(scene));
    }

    IEnumerator GameScene(Scene scene)
    {
        //화면이 서서히 까매진다.
        UIManager.Instance.FadeOut();
        //1.1초 동안 대기(화면이 검해지는 시간)
        yield return YieldInstructionCache.WaitForSeconds(1.1f);
        //현재 씬의 Claer 함수를 호출한다.
        CurrentScene.Clear();            
        SceneManager.LoadScene(GetSceneName(scene));
        //로딩 화면을 호출하고 씬을 변환한다.
        //UIManager.Instance.ShowPopupUi<LoadingScenePopupUI>().StartLoadSceneAsync(GetSceneName(scene));
    }

    string GetSceneName(Scene scene)
    {
        string name = System.Enum.GetName(typeof(Scene), scene);
        return name;
    }
}

public abstract class BaseScene : MonoBehaviour
{
    public Scene sceneType { get; protected set; } = Scene.Title;

    private void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
            ResourcesManager.Instance.Instantiate("EventSystem").name = "@EventSystem";
        Save();
    }

    public void Save()
    {
        if(DataManager.Instance.IsLoadPlayerInfo)
        {
            GPGSManager.Instance.SaveData();
        }
            
    }

    public abstract void Clear();
}
