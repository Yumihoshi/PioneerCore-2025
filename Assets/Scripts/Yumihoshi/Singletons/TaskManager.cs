// *****************************************************************************
// @author: Yumihoshi
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/28 16:12
// @version: 1.0
// @description:
// *****************************************************************************

using System.Collections.Generic;
using HoshiVerseFramework.Base;
using HoshiVerseFramework.FSM;
using HoshiVerseFramework.Singletons;
using Sirenix.OdinInspector;
using UnityEngine;
using Yumihoshi.Events;
using Yumihoshi.Task;

namespace Yumihoshi.Singletons
{
    public class TaskManager : Singleton<TaskManager>
    {
        [ShowInInspector]
        private readonly Dictionary<RoomType, int> _roomBuildIndexDict = new()
        {
            { RoomType.DaTingLevel1, 2 },
            { RoomType.XuanGuanLevel1, 3 },
            { RoomType.ZouLangLevel2, 4 },
            { RoomType.HuaShiLevel2, 5 },
            { RoomType.JianShenFangLevel2, 6 }
        };

        private FsmComponent _fsmComponent;

        protected override void Awake()
        {
            base.Awake();
            _fsmComponent = GetComponent<FsmComponent>();
        }

        private void OnEnable()
        {
            EventCenterManager.Instance.AddListener<OnEnterExitRoomEvent>(
                HandleOnEnterExitRoomEvent);
        }

        private void OnDisable()
        {
            EventCenterManager.Instance.RemoveListener<OnEnterExitRoomEvent>(
                HandleOnEnterExitRoomEvent);
        }

        /// <summary>
        /// 阶段一任务处理
        /// </summary>
        /// <param name="evt"></param>
        private void HandleOnEnterExitRoomEvent(OnEnterExitRoomEvent evt)
        {
            var tsk = _fsmComponent.FsmStateMachine.curState as Task1;
            if (!tsk)
            {
                Debug.LogWarning("[任务] 当前任务状态不是阶段一");
                return;
            }

            tsk.SetRoomStatus(evt.RoomType, evt.IsEnterOrExit);

            // 检查是否完成任务
            if (tsk.GetComplete())
            {
                _fsmComponent.FsmStateMachine.SwitchState("阶段二");
                return;
            }

            // 加载场景
            if (evt.IsEnterOrExit &&
                _roomBuildIndexDict.TryGetValue(evt.RoomType,
                    out int buildIndex))
                SceneLoader.Instance.LoadScene(
                    buildIndex);

            Debug.Log(
                $"[任务] 房间类型：{evt.RoomType} 是否进入：{evt.IsEnterOrExit} 完成：{tsk.GetComplete()}");
        }
    }
}
