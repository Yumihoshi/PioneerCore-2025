using IceFramework.UI;
using UnityEngine;
using UnityEngine.Events;

public class UIState : MonoBehaviour
{
    public UnityEvent Enter = new(); //进入当前状态时调用
    public UnityEvent Update = new(); //当前状态更新
    public UnityEvent Quit = new(); //退出当前状态时调用
    public UnityEvent Confirm = new(); //玩家输入确认时调用
    public UnityEvent<Vector2> Move = new(); //处理玩家输入

    public UIState()
    {
        //Enter.AddListener(PauseGame);//进入UI默认暂停游戏
        Enter.AddListener(_Enter);
        Update.AddListener(_Update);
        Quit.AddListener(_Quit);
        //Quit.AddListener(ContinueGame);//退出默认继续
        Confirm.AddListener(_Confirm);
        Move.AddListener(_Move);
    }

    protected virtual void _Enter()
    {
    }

    protected virtual void _Update()
    {
    }

    protected virtual void _Quit()
    {
    }

    public virtual void _Confirm()
    {
    }

    public virtual void _Move(Vector2 value)
    {
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
    }

    public void SetState()
    {
        UIManager.Instance.SetState(this);
    }
}
