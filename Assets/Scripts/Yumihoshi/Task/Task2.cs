// *****************************************************************************
// @author: Yumihoshi
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/28 16:16
// @version: 1.0
// @description:
// *****************************************************************************

using HoshiVerseFramework.FSM;
using UnityEngine;

namespace Yumihoshi.Task
{
    public class Task2 : FsmState
    {
        // TODO: 实现阶段逻辑
        public override bool OnCheck(StateContext context = null)
        {
            return true;
        }

        public override void OnEnter(StateContext context = null)
        {
            Debug.Log("[任务] 进入阶段二");
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
    }
}
