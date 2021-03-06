using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
public class MapManager : Managers<MapManager>
{
    Transform _root;

    public override void Init()
    {
        if(_root == null)
        {
            _root = new GameObject { name = "@Map_Root" }.transform;
        }
    }

    public void LoadMap(string stageName)
    {
        GameObject go = ResourcesManager.Instance.Instantiate($"Map/{stageName}");
        go.transform.parent = _root;
        MapController mapcontroller = go.GetOrAddComponent<MapController>();
        mapcontroller.speed = 30;
    }

}
