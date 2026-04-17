using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class LinearMenuBase : UIState
{
    public List<Option> Options = new();
    public UnityEvent MoveAction; //选择移动事件

    [ReadOnly] public int selectIndex;

    public void ResetSelectIndex()
    {
        selectIndex = 0;
    }


    public override void _Move(Vector2 value)
    {
        int optionNum = Options.Count;
        Options[selectIndex].DisSelectAction.Invoke();
        selectIndex += (int)value.x - (int)value.y; //左到右或上到下为加，反之为减
        selectIndex = selectIndex > optionNum - 1 ? selectIndex - optionNum :
            selectIndex < 0 ? selectIndex + optionNum : selectIndex;
        Options[selectIndex].OnSelectAction.Invoke();
        MoveAction?.Invoke();
    }

    public override void _Confirm()
    {
        Options[selectIndex].ConfirmAction.Invoke();
    }


    public void QuitEvent()
    {
        Quit.Invoke();
    }

    [Serializable]
    public class Option
    {
        public string name;
        public UnityEvent ConfirmAction; //按钮确认事件
        public UnityEvent OnSelectAction; //按钮被选择事件
        public UnityEvent DisSelectAction; //按钮取消选择事件
    }
}
