using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

//存档数据类

namespace IceFramework.SaveDataManager
{
    [Serializable]
    public abstract class SaveData
    {
        public string fileName { get; set; } = "SaveDateBase";
    }

    public class SettingData : SaveData
    {
        public int lastSaveId = 0;

        public SettingData()
        {
            fileName = "SettingData";
        }
    }


    public class PlayerData : SaveData
    {
        [ShowInInspector] [LabelText("角色字典")]
        public Dictionary<string, ActorInfo>
            ActorDictionary = new(); //一个字典，用于通过字符串来记录对应角色

        [ShowInInspector] [LabelText("背包数据")]
        public BagInfo BagInfo = new(); //背包信息

        [HideInInspector] public string GlobalBlackboard;

        [HideInInspector]
        public Dictionary<int, string> ObjectBlackboardDictionary = new();

        public PlayerData()
        {
            fileName = "PlayerData";
        }
    }
}
