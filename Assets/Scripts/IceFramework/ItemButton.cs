using IceFramework;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//用于显示在背包中的道具

public class ItemButton : MonoBehaviour, IPointerClickHandler,
    IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler,
    IPointerExitHandler
{
    public bool isSelect;
    [SerializeField] private Image Image;
    public Image composeImage;
    [SerializeField] private TMP_Text nameText, numText; //道具文本
    [SerializeField] private UnityEvent Enter, Exit;
    [SerializeField] private UnityEvent canCompose;
    [SerializeField] private UnityEvent InterFail;
    [SerializeField] private UnityEvent cantConfirm;

    [ShowInInspector] private ItemInfo itemInfo; //当前道具

    public ItemInfo ItemInfo
    {
        get => itemInfo;
        set
        {
            Image.sprite = value.Item.itemImage;
            nameText.text = value.Item.itemName;
            numText.text = value.num.ToString();
            itemInfo = value;
        }
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        //鼠标点击
        Confirm();
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        //鼠标按住
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        //鼠标进入
        Select();
        Enter.Invoke();
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        //鼠标离开
        Exit.Invoke();
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        //鼠标松开
    }

    public void UpdateButtonText()
    {
        Image.sprite = ItemInfo.Item.itemImage;
        nameText.text = ItemInfo.Item.itemName;
        numText.text = ItemInfo.num.ToString();
    }

    public void Select()
    {
        Bag.Instance.selectIndex = itemInfo.index;
        Bag.Instance.SetSelectImage();
        Bag.Instance.InfoTextUpdate();
    }

    public void Confirm() //确认
    {
        if (itemInfo.Item.HasFlag(Item.EItemType.组合使用))
        {
            isSelect = !isSelect;
            composeImage.gameObject.SetActive(isSelect);
            Bag.Instance.SelectItem(this);
            canCompose.Invoke();
        }
        else if (itemInfo.Item.HasFlag(Item.EItemType.直接使用))
        {
            Bag.Instance.Quit.Invoke(); //退出背包
            if (!ActorManager.Instance.ControlActor
                    .Interact(itemInfo)) //触发玩家交互事件
                //如果失败
                InterFail.Invoke();
        }
        else
        {
            cantConfirm.Invoke();
        }
    }
}
