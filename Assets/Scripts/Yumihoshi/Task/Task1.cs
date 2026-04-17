// *****************************************************************************
// @author: Yumihoshi
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/28 16:14
// @version: 1.0
// @description:
// *****************************************************************************

using System.Collections.Generic;
using HoshiVerseFramework.FSM;
using UnityEngine;

namespace Yumihoshi.Task
{
    /// <summary>
    /// 房间类型
    /// </summary>
    public enum RoomType
    {
        XuanGuanLevel1 = 1,
        LouTiJianLevel1,
        ChuWuJianLevel1,
        DaTingLevel1,
        ZouLangLevel2,
        HuaShiLevel2,
        JianShenFangLevel2
    }

    public class Task1 : FsmState
    {
        private Dictionary<RoomType, bool> _roomCompleteStatusDict;

        // TODO: 实现阶段逻辑，事件中心绑定
        public override bool OnCheck(StateContext context = null)
        {
            Debug.Log("[阶段任务] 阶段一检查成功");
            return true;
        }

        public override void OnEnter(StateContext context = null)
        {
            Debug.Log("[阶段任务] 进入阶段一");
            _roomCompleteStatusDict = new Dictionary<RoomType, bool>();
            _roomCompleteStatusDict.Add(RoomType.ChuWuJianLevel1, false);
            _roomCompleteStatusDict.Add(RoomType.HuaShiLevel2, false);
            _roomCompleteStatusDict.Add(RoomType.JianShenFangLevel2, false);
            _roomCompleteStatusDict.Add(RoomType.XuanGuanLevel1, false);
            _roomCompleteStatusDict.Add(RoomType.ZouLangLevel2, false);
        }

        public override void OnUpdate()
        {
        }

        public override void OnFixedUpdate()
        {
        }

        public override void OnExit(StateContext context = null)
        {
        }

        /// <summary>
        /// 获取阶段一任务进入所有房间是否完成
        /// </summary>
        /// <returns></returns>
        public bool GetComplete()
        {
            foreach (bool status in _roomCompleteStatusDict.Values)
                if (!status)
                    return false;

            return true;
        }

        /// <summary>
        /// 设置房间状态
        /// </summary>
        /// <param name="roomType"></param>
        /// <param name="status"></param>
        public void SetRoomStatus(RoomType roomType, bool status)
        {
            if (_roomCompleteStatusDict.ContainsKey(roomType))
            {
                _roomCompleteStatusDict[roomType] = status;
                Debug.Log(
                    $"[阶段任务] 房间{roomType}状态设置为{status}，当前房间状态：{_roomCompleteStatusDict[roomType]}");
            }
            else
            {
                Debug.LogError("[阶段任务] 房间类型不存在");
            }
        }
    }
}
