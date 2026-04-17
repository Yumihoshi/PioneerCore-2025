using System;
using System.Collections.Generic;
using System.Linq;
using IceFramework.SaveDataManager;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

//背包UI状态
[Serializable]
public class BagInfo
{
    [SerializeReference] [LabelText("背包物品")] [Tooltip("")]
    public Dictionary<string, ItemInfo> BagDictionary = new(); //背包字典
}

public class Bag : LinearMenuBase
{
    private static Bag _instance;

    [Title("背包子物体")] [SerializeField] [LabelText("道具UI预制体")] [Tooltip("")]
    private GameObject itemPrefab; //道具UI预制体

    [SerializeField] [LabelText("道具UI排列组")] [Tooltip("")]
    private Transform ItemGroup; //道具UI排列组

    [SerializeField] [LabelText("描述文本")] [Tooltip("")]
    private TMP_Text InfoText;

    [SerializeField] [LabelText("选择标识")] [Tooltip("")]
    private GameObject SelectImage;

    [SerializeField] [LabelText("道具UI组")] [Tooltip("")]
    private List<ItemButton> ItemButtons; //道具UI列表

    [SerializeField] [LabelText("背包容量")] [Tooltip("")]
    private int bagCapacity; //背包容量

    [LabelText("背包数据")] public BagInfo BagInfo;

    [Title("合成数据")] [SerializeField] [LabelText("选中的材料物品")] [Tooltip("")]
    private ItemButton[] selectItem = new ItemButton[2];

    public static Bag Instance
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = FindFirstObjectByType<Bag>();
            if (_instance != null) return _instance;
            _instance = new GameObject("Singleton of " + typeof(Bag))
                .AddComponent<Bag>();
            //_instance.UpBagData();

            return _instance;
        }
    }

    private void OnEnable()
    {
        BagInfo = SaveDataManagerObject.Instance.PlayerData.BagInfo;
        UpBagData();
    }

    public void Open()
    {
        gameObject.transform.localPosition = Vector3.zero;
    }

    public void Close()
    {
        gameObject.transform.localPosition = new Vector3(10000, 0, 0); //硬编码
    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void UpBagData() //道具UI实例更新
    {
        for (int i = 0; i < BagInfo.BagDictionary.Count; i++)
        {
            if (ItemButtons.Count - 1 < i) //如果背包中道具UI实例不够则创建一个
                ItemButtons.Add(Instantiate(itemPrefab, ItemGroup)
                    .GetComponent<ItemButton>());
            ItemButtons[i].GetComponent<ItemButton>().ItemInfo =
                BagInfo.BagDictionary.ElementAt(i).Value;
        }

        if (BagInfo.BagDictionary.Count < ItemButtons.Count)
            for (int i = ItemButtons.Count - 1;
                 i >= BagInfo.BagDictionary.Count;
                 i--)
            {
                GameObject targetObject = ItemButtons[i].gameObject;
                ItemButtons.RemoveAt(i);
#if UNITY_EDITOR
                if (Application.isPlaying)
                    Destroy(targetObject);
                else
                    DestroyImmediate(targetObject);
#else
                Destroy(targetObject);
#endif
            }

        ItemIndexUpdate();
    }

    /// <summary>
    /// 调整所有ItemUI的信息
    /// </summary>
    private void ItemIndexUpdate()
    {
        for (int i = 0; i < ItemButtons.Count; i++)
        {
            ItemButton itemButton = ItemButtons[i];
            itemButton.ItemInfo.index = i;
            itemButton.isSelect = false;
            itemButton.composeImage.gameObject.SetActive(false);
            itemButton.gameObject.name = itemButton.ItemInfo.Item.itemName;
        }
    }


    [Title("调试按钮")]
    [Button("清除背包")]
    public void Clear()
    {
        BagInfo.BagDictionary.Clear();
        ItemButtons.Clear();
        UpBagData();
    }

    /// <summary>
    /// 检查背包中的道具
    /// </summary>
    /// <param name="itemName">要检查的道具名</param>
    /// <returns>背包中是否有该道具</returns>
    [Button("检查道具")]
    public bool Check(string itemName)
    {
        return BagInfo.BagDictionary.ContainsKey(itemName);
    }

    /// <summary>
    /// 添加指定数量道具到背包
    /// </summary>
    /// <param name="item">道具</param>
    /// <param name="num">数量（留空为1）</param>
    /// <returns>是否添加成功</returns>
    [Button("添加道具")]
    public bool Add(Item item, int num = 1)
    {
        if (BagInfo.BagDictionary.TryGetValue(item.itemName, out ItemInfo info))
        {
            info.num += num;
            ItemButtons[info.index].UpdateButtonText();
            return true;
        }

        if (BagInfo.BagDictionary.Count < bagCapacity)
        {
            BagInfo.BagDictionary[item.itemName] = new ItemInfo(item.itemName,
                num, BagInfo.BagDictionary.Count);
            UpBagData();
            return true;
        }

        return false;
    }

    /// <summary>
    /// 移除指定数量道具
    /// </summary>
    /// <param name="item">道具</param>
    /// <param name="num">数量</param>
    [Button("移除道具")]
    public void Remove(Item item, int num = 1)
    {
        if (BagInfo.BagDictionary.ContainsKey(item.itemName))
        {
            ItemInfo info = BagInfo.BagDictionary[item.itemName];
            if (info.num > num)
                info.num -= num;
            else if (info.num == num)
                BagInfo.BagDictionary.Remove(item.itemName);
            UpBagData();
        }
    }

    /// <summary>
    /// 移除指定的道具
    /// </summary>
    /// <param name="item">道具</param>
    public void RemoveAll(Item item)
    {
        if (BagInfo.BagDictionary.ContainsKey(item.itemName))
        {
            BagInfo.BagDictionary.Remove(item.itemName);
            UpBagData();
        }
    }

    /// <summary>
    /// 更新物品介绍信息
    /// </summary>
    public void InfoTextUpdate()
    {
        if (ItemButtons[selectIndex].ItemInfo != null)
            InfoText.text =
                ItemButtons[selectIndex].ItemInfo.Item.itemIntroduction;
        else
            InfoText.text = "";
    }

    /// <summary>
    /// 选中物体，用于合成
    /// </summary>
    /// <param name="itemButton">对应物体UI</param>
    public void SelectItem(ItemButton itemButton)
    {
        if (selectItem[0] != null)
        {
            if (selectItem[0] != itemButton)
            {
                selectItem[1] = itemButton;
                ItemCompose();
            }
            else
            {
                selectItem[0] = null;
            }
        }
        else if (selectItem[1] != null)
        {
            if (selectItem[1] != itemButton)
            {
                selectItem[0] = itemButton;
                ItemCompose();
            }
            else
            {
                selectItem[1] = null;
            }
        }
        else
        {
            selectItem[0] = itemButton;
        }
    }

    /// <summary>
    /// 合成判断
    /// </summary>
    private void ItemCompose()
    {
        if (selectItem[0].ItemInfo.Item.stuffItem ==
            selectItem[1].ItemInfo.Item)
        {
            //合成成功
            Item composeItem = selectItem[0].ItemInfo.Item.ComposeItem;
            SelectImage.SetActive(false);
            SelectImage.transform.SetParent(transform, false);
            _RemoveNum(selectItem[0].ItemInfo.Item);
            Remove(selectItem[1].ItemInfo.Item);
            Add(composeItem);
            selectIndex = BagInfo.BagDictionary[composeItem.itemName].index;
            SetSelectImage();
            InfoTextUpdate();
        }

        //合成失败
        selectItem[0] = null;
        selectItem[1] = null;

        void _RemoveNum(Item item, int num = 1)
        {
            if (BagInfo.BagDictionary.ContainsKey(item.itemName))
            {
                ItemInfo info = BagInfo.BagDictionary[item.itemName];
                if (info.num > num)
                    info.num -= num;
                else if (info.num == num)
                    BagInfo.BagDictionary.Remove(item.itemName);
            }
        }
    }

    public override void _Move(Vector2 value)
    {
        if (value.x != 0)
        {
            if (selectIndex % 2 != 0)
                selectIndex--;
            else
                selectIndex++;
        }

        selectIndex -= 2 * (int)value.y;
        if (selectIndex < 0)
        {
            selectIndex = 0;
        }
        else if (selectIndex > ItemButtons.Count - 1)
        {
            if (selectIndex % 2 != 0)
                selectIndex = ItemButtons.Count - 1;
            else
                selectIndex = ItemButtons.Count - 2;
        }

        ItemButtons[selectIndex].Select();
        SetSelectImage();
        InfoTextUpdate();
    }

    public override void _Confirm()
    {
        ItemButtons[selectIndex].Confirm();
    }

    public void SetSelectImage()
    {
        SelectImage.SetActive(true);
        SelectImage.transform.SetParent(
            ItemButtons[selectIndex].gameObject.transform, false);
    }
}

[Searchable]
public class ItemInfo
{
    public int index; //每个item对应的索引，和UI交互有关

    [JsonIgnore] private Item item;

    public string ItemName;
    public int num;

    public ItemInfo(string itemName, int num, int index)
    {
        Debug.Log(itemName);
        ItemName = itemName;
        this.num = num;
        this.index = index;
        Item.UseEvent.AddListener(beUsed);
    }

    [JsonIgnore]
    public Item Item
    {
        get
        {
            if (item == null) GetItemFromName();
            return item;
        }
    }

    private void GetItemFromName()
    {
        // 加载资源
        Item item = Resources.Load<Item>($"Items/{ItemName}");
        if (item == null) Debug.LogError($"道具{ItemName}不存在");
        this.item = item;
    }

    private void beUsed()
    {
        if (Item.HasFlag(Item.EItemType.消耗品)) Bag.Instance.Remove(Item);
    }
}
