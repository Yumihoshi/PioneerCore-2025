using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

//道具数据

[CreateAssetMenu(fileName = "新道具", menuName = "创建道具")]
public class Item : ScriptableObject
{
    [Flags]
    public enum EItemType
    {
        None = 0,
        直接使用 = 1 << 0,
        自动使用 = 1 << 1,
        组合使用 = 1 << 2,
        消耗品 = 1 << 3
    }

    [LabelText("道具名")] public string itemName;
    [LabelText("道具图片")] public Sprite itemImage;
    [LabelText("道具种类")] public EItemType ItemType;

    [LabelText("同级原料")] [Tooltip("如果该物品属于合成类型，需要在这里将另一个原料物品填入")]
    public Item stuffItem; //材料

    [LabelText("上级合成")] public Item ComposeItem; //合成物

    [LabelText("道具事件")] [Tooltip("直接使用时触发的事件")]
    public UnityEvent UseEvent;

    [FormerlySerializedAs("itemInfo")] [TextArea] [LabelText("道具描述")]
    public string itemIntroduction;

    public bool HasFlag(EItemType flag)
    {
        return (ItemType & flag) == flag;
    }

    public void Add(EItemType flag)
    {
        ItemType |= flag;
    }

    public void Remove(EItemType flag)
    {
        ItemType &= ~flag;
    }
}
