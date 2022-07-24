using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Utils 
{
    public static T CreateObject<T>(Transform parent) where T : Component
    {
        GameObject obj = new GameObject(typeof(T).Name, typeof(T));
        obj.transform.SetParent(parent);
        T t = obj.GetComponent<T>();
        return t;
    }

    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
        {
            component = go.AddComponent<T>();
        }
        return component;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;
        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                {
                    return component;
                }
            }
        }
        return null;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform != null)
            return transform.gameObject;
        return null;
    }

    public static Button BindingFunc(Transform transform, UnityAction action)
    {
        Button button = transform.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(action);
        }
        return button;
    }

    public static T FindObjectOfType<T>(bool init = false) where T : Component
    {
        T t = GameObject.FindObjectOfType<T>();
        if(t != null)
        {
            if (init)
                t.transform.SendMessage("Init", SendMessageOptions.DontRequireReceiver);
        }
        return t;
    }
}

public class UIHelper
{
    public static Vector2 WorldPosToMapPos(Vector3 worldPos, float worldWidth, float worldDepth, float mapWidth, float mapHeight)
    {
        Vector3 result = Vector3.zero;
        result.x = (worldPos.x * mapWidth) / worldWidth;
        result.y = (worldPos.z * mapHeight) / worldDepth;
        return result;
    }

    public static Vector3 MapPosToWorldPos(Vector3 uiPos, float worldWidth, float worldDepth, float mapWidth, float mapHeight)
    {
        Vector3 result = Vector3.zero;
        result.x = (uiPos.x * worldWidth) / mapWidth;
        result.y = (uiPos.y * worldDepth) / mapHeight;
        return result;
    }

    public static void MarkOnAMap(Transform world, Transform UITarget, float worldWidth, float worldDepth, float mapWidth, float mapHeight)
    {
        UITarget.localPosition = WorldPosToMapPos(world.position, worldWidth, worldDepth, mapWidth, mapHeight);
        MarkOnAMap(world, UITarget);
    }

    public static void MarkOnAMap(Transform world, Transform UITarget)
    {
        float angleZ = Mathf.Atan2(world.forward.z, world.forward.x) * Mathf.Rad2Deg;
        UITarget.eulerAngles = new Vector3(0, 0, angleZ - 90);
    }

    public static void MarkOnTheRPGGame(Vector3 world, Transform uiBackground, float worldWidth, float worldDepth, float mapWidth, float mapHeight)
    {
        uiBackground.localPosition = WorldPosToMapPos(world, worldWidth, worldDepth, mapWidth, mapHeight) * -1;
    }
}

public static class Extention
{

    //public static void AddUIEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvents type = Define.UIEvents.Click)
    //{
    //    BaseUI.BindEvent(go, action, type);
    //}

    public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
        return Utils.GetOrAddComponent<T>(go);
    }
}
