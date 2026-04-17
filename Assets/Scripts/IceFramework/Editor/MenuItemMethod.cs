#if UNITY_EDITOR

using System.Collections.Generic;
using IceFramework.ObjectsPoolManager;
using IceFramework.TimerManager;
using UnityEditor;
using UnityEngine;

public static class MenuItemMethod
{
    #region LevelDesign

    [MenuItem("GameObject/添加预制体 #z", false, 1)] // 添加快捷键:Shift+Z
    private static void CreatePrefabMenu()
    {
        // 获取选中的文件夹路径
        string folderPath = "Assets/Prefabs";
        string prefabExtension = ".prefab";
        if (!AssetDatabase.IsValidFolder(folderPath)) return;

        // 动态创建菜单
        GenericMenu menu = new GenericMenu();

        // 扫描文件夹内的预制体
        var prefabGuids =
            AssetDatabase.FindAssets("t:Prefab", new[] { folderPath });
        foreach (var guid in prefabGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            // 加载预制体缩略图
            Texture icon = AssetPreview.GetAssetPreview(prefab);
            // 添加菜单项
            menu.AddItem(
                new GUIContent(
                    path.Replace(folderPath + "/", "")
                        .Replace(prefabExtension, ""), icon),
                false,
                () => InstantiatePrefab(prefab)
            );
        }

        menu.ShowAsContext();
    }

    #endregion

    #region Init

    [MenuItem("IceFramework/创建对象池管理器", false, 1)]
    private static void CreateObjectPoolManager()
    {
        new ObjectsPool();
    }

    [MenuItem("IceFramework/创建计时器管理器", false, 2)]
    private static void CreateTimerManager()
    {
        new TimerManager();
    }

    #endregion

    #region PrivateMethon

    private static List<GameObject>
        FindObjectsByTag(string tag) //根据tag在Hierarchy里找到物体,包括隐藏的物体
    {
        List<GameObject> gameObjects = new List<GameObject>();
        foreach (GameObject go in Resources.FindObjectsOfTypeAll(
                     typeof(GameObject)))
            if (!EditorUtility.IsPersistent(go.transform.root.gameObject) &&
                !(go.hideFlags == HideFlags.NotEditable ||
                  go.hideFlags == HideFlags.HideAndDontSave))
                if (go.tag == tag)
                    gameObjects.Add(go);

        return gameObjects;
    }

    private static void InstantiatePrefab(GameObject prefab) //在场景中添加物体
    {
        if (prefab == null) return;

        // 在场景中创建实例
        GameObject instance =
            PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        Undo.RegisterCreatedObjectUndo(instance, "Instantiate Prefab");
        instance.transform.parent = Selection.activeTransform;
        Selection.activeObject = instance;
        instance.transform.localPosition = Vector3.zero;
    }

    #endregion
}

#endif
