using System.Collections.Generic;
using IceFramework;
using IceFramework.SaveDataManager;
using NodeCanvas.Framework;
using UnityEngine;

public class GlobalBlackboardManager : MonoSingleton<GlobalBlackboardManager>
{
    public GlobalBlackboard GlobalBlackboard;
    public string BlackboardJson;

    private void OnEnable()
    {
        GlobalBlackboard.Deserialize(
            SaveDataManagerObject.Instance.PlayerData.GlobalBlackboard,
            new List<Object>());
    }

    private void OnDisable()
    {
        SaveDataManagerObject.Instance.PlayerData.GlobalBlackboard =
            GlobalBlackboard.Serialize(new List<Object>());
    }
}
