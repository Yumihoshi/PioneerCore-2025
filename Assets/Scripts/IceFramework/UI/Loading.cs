using System.IO;
using IceFramework.SaveDataManager;
using TMPro;

public class Loading : LinearMenuBase
{
    private readonly bool[] SaveFileInfos = new bool[3];
    private TMP_Text[] Buttons;

    private void Start()
    {
        Init();
        ButtonUpdate();
    }

    private void Init() //获取存档状态
    {
        for (int i = 0; i < SaveFileInfos.Length; i++)
        {
            string filePath = SaveDataManager.Instance.GetSavePath(i + 1);
            SaveFileInfos[i] = Directory.Exists(filePath);
        }
    }

    private void ButtonUpdate()
    {
        Buttons = GetComponentsInChildren<TMP_Text>();
        for (int i = 0; i < Buttons.Length; i++)
            if (!SaveFileInfos[i])
                Buttons[i].text = "创建存档";
    }

    public void ResetSavePath()
    {
        SettingData SettingData =
            SaveDataManager.Instance.LoadSetting<SettingData>(); //读取设置文件
        SettingData.lastSaveId = selectIndex + 1; //修改id
        SaveDataManager.Instance.SaveSetting(SettingData); //更新设置文件的上次加载存档id
        SaveDataManager.Instance.SavePathRename(selectIndex + 1); //重命名路径
        if (!SaveFileInfos[selectIndex]) CreatSave(selectIndex + 1);
    }

    private void CreatSave(int id)
    {
        SaveDataManager.Instance.CheckAndCreatSaveDir(); //创建目录
        SaveDataManager.Instance.WriteDefaultData(); //写入默认存档
    }
}
