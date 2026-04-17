using System.IO;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using Yumihoshi.Singletons;

//存档数据管理器
//可以进行类的序列和反序列化
//类中的数据需要在其他脚本处理

namespace IceFramework.SaveDataManager
{
    public class SaveDataManagerObject : MonoSingleton<SaveDataManagerObject>
    {
        [ShowInInspector] public PlayerData PlayerData;

        private SettingData SettingData;

        private void OnApplicationQuit()
        {
#if UNITY_EDITOR
            SaveData();
#endif
        }

        protected override void Init() //初始化路径，创建目录，写入默认设置存档
        {
            SaveDataManager.Instance.PathInit(); //获取除存档外的路径
            SaveDataManager.Instance.CheckAndCreateSettingDir(); //检查路径并创建设置目录
            if (!SaveDataManager.Instance
                    .TryLoadSetting(out SettingData)) //尝试读取设置存档
            {
                SaveDataManager.Instance
                    .WriteDefaultSettingData(); //如果没有则写入默认设置存档
                SettingData =
                    SaveDataManager.Instance.LoadSetting<SettingData>();
            }

            SaveDataManager.Instance.SavePathRename(SettingData
                .lastSaveId); //从设置存档获取上次加载的游戏存档id，并获取存档路径
        }

        [Button]
        private void CreatJson(SaveData saveData)
        {
            string path = "StreamingAssets/";
            path = Path.Combine(Application.dataPath, path);
            SaveDataManager.Instance.SaveJson(saveData,
                path + saveData.fileName);
        }

        [Button]
        public void LoadData()
        {
            if (!SaveDataManager.Instance.TryLoad(out PlayerData))
                PlayerData = new PlayerData();
        }

        [Button]
        public void SaveData()
        {
            SaveDataManager.Instance.Save(PlayerData);
        }

        [Button]
        public void StartGame() //开始游戏
        {
            CheckSaveFile();
            LoadData(); //加载游戏存档

            SceneLoader.Instance.LoadScene(0);

            void CheckSaveFile() //用于开始游戏时检查存档文件
            {
                switch (SettingData.lastSaveId)
                {
                    case 0: //如果是0，则说明是第一次启动游戏
                        SettingData.lastSaveId = 1; //修改id为1
                        SaveDataManager.Instance.SavePathRename(1); //重命名路径
                        SaveDataManager.Instance.CheckAndCreatSaveDir(); //创建目录
                        //SaveDataManager.Instance.WriteDefaultData(); //写入默认存档
                        SaveDataManager.Instance
                            .SaveSetting(SettingData); //更新设置文件的上次加载存档id
                        break;
                    default: //其他情况
                        if (!Directory.Exists(
                                SaveDataManager.Instance.GetSavePath(SettingData
                                    .lastSaveId))) //如果存档路径不存在
                            SaveDataManager.Instance
                                .CheckAndCreatSaveDir(); //创建目录
                        //SaveDataManager.Instance.WriteDefaultData(); //写入默认存档
                        break;
                }
            }
        }
    }

    public class SaveDataManager : SingletonBace<SaveDataManager>
    {
        private const string saveDirName = "saveData";
        private const string settingDirName = "_settingData";

        private string defaultSavePath =
            Path.Combine(Application.streamingAssetsPath,
                saveDirName); //默认游戏存档路径

        private string defaultSettingPath =
            Path.Combine(Application.streamingAssetsPath,
                settingDirName); //默认设置存档路径

        private string saveDirPath; //玩家游戏存档路径

        private string settingDirPath =
            Path.Combine(Application.persistentDataPath,
                settingDirName); //玩家设置存档路径

        public void PathInit() //路径初始化
        {
            defaultSavePath = Path.Combine(Application.streamingAssetsPath,
                saveDirName);
            settingDirPath = Path.Combine(Application.persistentDataPath,
                settingDirName);
            defaultSettingPath = Path.Combine(Application.streamingAssetsPath,
                settingDirName);
        }

        public void SavePathRename(int id = 0)
        {
            saveDirPath = Path.Combine(Application.persistentDataPath,
                saveDirName + id);
        }

        public string GetSavePath(int id)
        {
            return Path.Combine(Application.persistentDataPath,
                saveDirName + id);
        }

        public void CheckAndCreateSettingDir() //检查并创建设置目录
        {
            if (Directory.Exists(settingDirPath) == false)
                Directory.CreateDirectory(settingDirPath);
        }

        public void CheckAndCreatSaveDir() //检查并创建存档目录
        {
            if (Directory.Exists(saveDirPath) == false)
                Directory.CreateDirectory(saveDirPath);
        }

        public void WriteDefaultSettingData() //默认设置存档写入
        {
            foreach (string filePath in Directory.GetFiles(defaultSettingPath))
            {
                if (filePath.EndsWith(".meta")) continue;
                string fileName = Path.GetFileName(filePath);
                string destPath = Path.Combine(settingDirPath, fileName);
                //Debug.Log(destPath);
                File.WriteAllText(destPath, File.ReadAllText(filePath));
            }
        }

        public void WriteDefaultData() //默认游戏存档写入
        {
            foreach (string filePath in Directory.GetFiles(defaultSavePath))
            {
                if (filePath.EndsWith(".meta")) continue;
                string fileName = Path.GetFileName(filePath);
                string destPath = Path.Combine(saveDirPath, fileName);
                Debug.Log(destPath);
                File.WriteAllText(destPath, File.ReadAllText(filePath));
            }
        }

        #region Setting:Save&Load&Delete

        public void SaveSetting(object settingData, string fileName)
        {
            SaveJson(settingData, Path.Combine(settingDirPath, fileName));
        }

        public void SaveSetting(object settingData)
        {
            SaveJson(settingData,
                Path.Combine(settingDirPath, settingData.GetType().Name));
        }

        public T LoadSetting<T>(string fileName) where T : class
        {
            //Debug.Log(Path.Combine(settingDirPath ,fileName));
            return LoadJson<T>(Path.Combine(settingDirPath, fileName));
        }

        public T LoadSetting<T>() where T : class
        {
            return LoadSetting<T>(typeof(T).Name);
        }

        public bool TryLoadSetting<T>(string fileName, out T Date)
            where T : class, new()
        {
            return TryLoadJson(Path.Combine(settingDirPath, fileName),
                out Date);
        }

        public bool TryLoadSetting<T>(out T Date) where T : class, new()
        {
            return TryLoadSetting(typeof(T).Name, out Date);
        }

        public void DeleteSetting(string fileName)
        {
            DeleteJson(settingDirPath + fileName);
        }

        public void DeleteSetting(object settingData)
        {
            DeleteSetting(settingData.GetType().Name);
        }

        #endregion

        #region Game:Save&Load&Delete

        public void Save(object data, string fileName)
        {
            SaveJson(data, Path.Combine(saveDirPath, fileName));
        }

        public void Save(object data)
        {
            if (data == null)
            {
                Debug.LogError("要保存的数据为空");
                return;
            }

            Save(data, data.GetType().Name);
        }

        public T Load<T>(string fileName) where T : class
        {
            //Debug.Log(Path.Combine(saveDirPath ,fileName));
            return LoadJson<T>(Path.Combine(saveDirPath, fileName));
        }

        public T Load<T>() where T : class
        {
            return Load<T>(typeof(T).Name);
        }

        public bool TryLoad<T>(string fileName, out T Date)
            where T : class, new()
        {
            Debug.Log(saveDirPath);
            return TryLoadJson(Path.Combine(saveDirPath, fileName), out Date);
        }

        public bool TryLoad<T>(out T Date) where T : class, new()
        {
            return TryLoad(typeof(T).Name, out Date);
        }

        public void Delete(string fileName)
        {
            DeleteJson(Path.Combine(saveDirPath, fileName));
        }

        public void Delete(object data)
        {
            DeleteJson(Path.Combine(saveDirPath, data.GetType().Name));
        }

        #endregion

        #region DefaultData

        public T LoadDefaultDate<T>(string fileName) where T : class
        {
            //Debug.Log(Path.Combine(defaultSavePath ,fileName));
            return LoadJson<T>(Path.Combine(defaultSavePath, fileName));
        }

        public T LoadDefaultDate<T>() where T : class
        {
            return LoadDefaultDate<T>(typeof(T).Name);
        }

        #endregion

        #region PrivateMethod

        public void SaveJson(object saveObject, string path)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
#if UNITY_EDITOR
            string jsonData = JsonConvert.SerializeObject(saveObject,
                Formatting.Indented, settings);
#else
            string jsonData = JsonConvert.SerializeObject(saveObject,settings);
#endif
            //print(path);
            File.WriteAllText(path, jsonData);
        }

        private T LoadJson<T>(string path) where T : class
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            if (File.Exists(path))
                return JsonConvert.DeserializeObject<T>(File.ReadAllText(path),
                    settings);
            return null;
        }

        private bool TryLoadJson<T>(string path, out T Date)
            where T : class, new()
        {
            if (File.Exists(path))
            {
                Date = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
                return true;
            }

            Date = new T();
            return false;
        }

        private void DeleteJson(string path)
        {
            if (File.Exists(path)) File.Delete(path);
        }

        #endregion
    }
}
