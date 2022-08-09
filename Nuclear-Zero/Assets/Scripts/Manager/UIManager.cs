using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIList
{

}

public class UIManager : Managers<UIManager>
{
    static int _order = -1;
    //Dictionary<UIList, BaseUI> UIDic = new Dictionary<UIList, BaseUI>();
    SceneUI _sceneUI = null;
    Stack<PopupUI> _popupStack = new Stack<PopupUI>();

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("UI_Root");
            if(root == null)
            {
                root = new GameObject { name = "UI_Root" };
            }
            return root;
        }
    }

    public T Get<T>(string name = null,bool recursive = false) where T : BaseUI
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }
        return Utils.FindChild<T>(Root, name, recursive);
    }

    public void SetCanvas(GameObject go,bool sort = true)
    {
        Canvas canvas = Utils.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;
        //canvas.worldCamera = Camera.main;
        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
            canvas.sortingOrder = 0;
    }

    public void FadeIn(float targetTime = 1.0f)
    {
        FadePopupUI fade = ShowPopupUi<FadePopupUI>();
        if (fade != null)
            fade.FadeIn(targetTime);
    }

    public void FadeOut(float targetTime = 1.0f)
    {
        FadePopupUI fade = ShowPopupUi<FadePopupUI>();
        if (fade != null)
            fade.FadeOut(targetTime);
    }

    public T MakeWorldSpaceUI<T>(Transform parent = null,string name = null) where T : BaseUI
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }
        GameObject go = ResourcesManager.Instance.Instantiate($"UI/WorldSpace/{name}");
        if(parent != null)
        {
            go.transform.SetParent(parent);
        }
        Canvas _canvas = go.GetOrAddComponent<Canvas>();
        _canvas.renderMode = RenderMode.WorldSpace;
        _canvas.worldCamera = Camera.main;

        return Utils.GetOrAddComponent<T>(go);
    }

    public T MakeSubItem<T>(Transform parent = null,string name = null) where T : SubUI
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }
        GameObject go = ResourcesManager.Instance.Instantiate($"UI/SubUI/{name}");
        if(parent != null)
        {
            go.transform.SetParent(parent);
        }
        return Utils.GetOrAddComponent<T>(go);
    }

    public T ShowSceneUi<T>(string name = null) where T : SceneUI
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;
        GameObject go = ResourcesManager.Instance.Instantiate($"UI/SceneUI/{name}");
        T sceneUI = Utils.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI;

        go.transform.SetParent(Root.transform);
        _sceneUI.Init();

        return sceneUI;
    }

    public T ShowPopupUi<T>(string name = null) where T : PopupUI
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;
        GameObject go = ResourcesManager.Instance.Instantiate($"UI/PopupUI/{name}");
        T popup = Utils.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);

        go.transform.SetParent(Root.transform);
        popup.Init();

        return popup;
    }

    public void ClosePopupUI(PopupUI popup)
    {
        if (_popupStack.Count == 0)
            return;

        if (_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed");
            return;
        }
        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;

        PopupUI popup = _popupStack.Pop();
        if(popup != null)
        {
            ResourcesManager.Instance.Destroy(popup.gameObject);
            popup = null;
        }
    }

    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }

    public void SetUIActive(bool state)
    {
        _sceneUI.gameObject.SetActive(state);
        foreach(PopupUI popup in _popupStack.ToArray())
        {
            if(popup != null)
            {
                popup.gameObject.SetActive(state);
            }
        }
    }

    public void Clear()
    {
        CloseAllPopupUI();
        _sceneUI = null;
        _order = -10;
    }
}
