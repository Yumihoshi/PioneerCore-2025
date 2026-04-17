using UnityEngine;

public class Idle : UIState
{
    private static Idle _instance;
    public float hp;

    public ItemInfo[] ShortCutItem = new ItemInfo[3]; //快捷列表道具数据组

    public static Idle Instance
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = FindFirstObjectByType<Idle>();
            if (_instance != null) return _instance;
            _instance = new GameObject("Singleton of " + typeof(Bag))
                .AddComponent<Idle>();

            return _instance;
        }
    }

    public void SetShortCut(ItemInfo itemInfo, int index)
    {
        if (itemInfo.Item.HasFlag(Item.EItemType.直接使用))
            ShortCutItem[index] = itemInfo;
        else
            print("道具无法添加至快捷栏，因为不是直接使用的道具");
    }
}
