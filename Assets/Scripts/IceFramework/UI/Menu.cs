using System.IO;
using IceFramework.SaveDataManager;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class Menu : LinearMenuBase
{
    [SerializeField] [ShowInInspector] private TMP_Text PlayButton;

    private SettingData SettingData;

    private void Start()
    {
        CheckSaveFile();
    }

    public void CheckSaveFile()
    {
        SettingData =
            SaveDataManager.Instance
                .LoadSetting<SettingData>(); //读取设置文件，获取上次加载存档id
        if (!Directory.Exists(
                SaveDataManager.Instance.GetSavePath(SettingData
                    .lastSaveId))) //如果上次加载存档id对应文件不存在
            PlayButton.text = "开始游戏"; //修改按钮文本
    }
}
