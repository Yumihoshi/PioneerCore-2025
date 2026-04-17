using System;
using System.Collections.Generic;
using DG.Tweening;
using IceFramework;
using IceFramework.SaveDataManager;
using NodeCanvas.Framework;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

//角色基类
[Serializable]
public class ActorInfo
{
    public float hp;
    public float san;
    public Vector2 pos;
    public Vector2Int faceDirection; //面朝方向
    public string BlackboardJson;
}

public class Actor : MonoBehaviour
{
    public enum EActorState
    {
        Idle,
        Moving
    }

    private static readonly int IsMoveID = Animator.StringToHash("IsMove");
    private static readonly int InputXID = Animator.StringToHash("InputX");
    private static readonly int InputYID = Animator.StringToHash("InputY");

    [LabelText("角色名")] public string actorName;
    public ActorInfo ActorInfo;

    [SerializeField] [LabelText("阻碍层级")] [Tooltip("不可移动的层级")]
    private LayerMask CantMove;

    [SerializeField] [LabelText("交互层级")] [Tooltip("可交互的层级")]
    private LayerMask Interactable; //不可移动的层级 可以互动层级

    [SerializeField] [LabelText("移动事件")] [Tooltip("当玩家每次移动时触发")]
    public UnityEvent MoveEvent;

    [SerializeField] [LabelText("阻碍事件")] [Tooltip("玩家被阻碍时触发")]
    public UnityEvent CantMoveEvent;

    private Animator _animator;

    private Blackboard Blackboard;
    private Collider2D Collider2D;

    [ShowInInspector]
    [LabelText("玩家移动状态")]
    public EActorState ActorState { get; private set; } = EActorState.Idle;

    public Vector2 Pos //游戏内所有对位置的修改都要通过该属性
    {
        get => ActorInfo.pos;
        set
        {
            ActorState = EActorState.Moving;
            transform
                .DOMove(value, ActorManager.Instance.MoveAnimationDate.time)
                .SetEase(ActorManager.Instance.MoveAnimationDate.Ease)
                .OnComplete(() => { ActorState = EActorState.Idle; });
            ActorInfo.pos = value;
        }
    }

    private void Awake()
    {
        Collider2D = GetComponent<Collider2D>();
        Blackboard = GetComponent<Blackboard>();
        _animator = GetComponent<Animator>();
    }

    public void OnEnable()
    {
        Load();
    }

    public void OnDisable()
    {
        Save();
    }

    private void Load()
    {
        if (!SaveDataManagerObject.Instance.PlayerData.ActorDictionary
                .ContainsKey(actorName))
        {
            SaveDataManagerObject.Instance.PlayerData.ActorDictionary
                [actorName] = ActorInfo;
            return;
        }

        ActorInfo actorInfo =
            SaveDataManagerObject.Instance.PlayerData
                .ActorDictionary[actorName];
        ActorInfo = actorInfo;
        // TODO 之后在这里根据面朝方向修改角色贴图
        transform.position = actorInfo.pos;
        Blackboard.Deserialize(actorInfo.BlackboardJson, new List<Object>());
    }

    private void Save()
    {
        ActorInfo.BlackboardJson = Blackboard.Serialize(new List<Object>());
    }

    /// <summary>
    /// 移动：
    /// </summary>
    /// <param name="direction">移动的方向（单位向量）</param>
    /// 如果移动的方向上没有阻碍的话，角色会进行移动同时触发移动事件，否则触发不可移动事件
    public Actor Move(Vector2Int direction)
    {
        if (direction == Vector2Int.zero) return this; //如果当前没有检测到输入则直接返回
        ActorInfo.faceDirection = direction; //修改面朝方向
        if (ActorState == EActorState.Idle &&
            !CheckDirection(direction, CantMove))
        {
            //可移动
            MoveEvent.Invoke();
            Pos += direction;
        }
        else
        {
            CantMoveEvent.Invoke();
        }

        return this;
    }

    /// <summary>
    /// 互动
    /// </summary>
    /// <param name="itemInfo">道具信息</param>
    /// <returns>前方是否有可交互物体</returns>
    public bool Interact(ItemInfo itemInfo = null)
    {
        GlobalBlackboardManager.Instance.GlobalBlackboard.SetVariableValue(
            "UseItemName", itemInfo?.Item.name);
        itemInfo?.Item.UseEvent.Invoke();
        if (CheckDirection(ActorInfo.faceDirection, Interactable,
                out Collider2D objectCollider))
            if (objectCollider.TryGetComponent(
                    out InteractableObject interactableObject))
            {
                interactableObject.InterEvent.Invoke();
                GlobalBlackboardManager.Instance.GlobalBlackboard
                    .SetVariableValue("UseItemName", false);
                return true;
            }

        GlobalBlackboardManager.Instance.GlobalBlackboard.SetVariableValue(
            "UseItemName", false);
        return false;
    }

    /// <summary>
    /// 判断指定方向物体
    /// </summary>
    /// <param name="direction">方向</param>
    /// <param name="layerMask">检测层级</param>
    /// <param name="Collider2D">输出碰撞体</param>
    /// <returns>指定方向是否有物体</returns>
    private bool CheckDirection(Vector2Int direction, LayerMask layerMask,
        out Collider2D Collider2D)
    {
        //在目标方向的格子上创建一条射线来获取对应碰撞体
        RaycastHit2D[] hit2D =
            Physics2D.RaycastAll(Pos, direction, 1f, layerMask);
        foreach (RaycastHit2D value in hit2D)
            if (value.collider != this.Collider2D)
            {
                Collider2D = value.collider;
                return true;
            }

        Collider2D = null;
        return false;
    }

    private bool CheckDirection(Vector2Int direction, LayerMask layerMask)
    {
        //在目标方向的格子上创建一条射线来获取对应碰撞体
        RaycastHit2D[] hit2D =
            Physics2D.RaycastAll(Pos, direction, 1f, layerMask);
        foreach (RaycastHit2D value in hit2D)
            if (value.collider != Collider2D)
                return true;

        return false;
    }
}
