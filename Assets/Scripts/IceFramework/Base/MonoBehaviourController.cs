using System.Collections.Generic;
using UnityEngine.Events;

namespace IceFramework
{
    /*
     * Mono管理类，不继承Mono的脚本需要使用Mono的周期函数或功能可调用该脚本，比如更新周期，协程调用
     */
    public class
        MonoBehaviourController : MonoSingleton<MonoBehaviourController>
    {
        private UnityAction updateUnityAction;

        private List<UnityAction> updateUnityActionList;

        private void Update()
        {
            updateUnityAction?.Invoke();
        }

        protected override void Init()
        {
            base.Init();
            updateUnityActionList = new List<UnityAction>();
        }

        /// <summary>
        /// 添加指定更新事件
        /// </summary>
        /// <param ListenerName="action"></param>
        public void AddUpdateAction(UnityAction action)
        {
            updateUnityAction += action;
            updateUnityActionList.Add(action);
        }

        /// <summary>
        /// 删除指定更新事件
        /// </summary>
        /// <param ListenerName="action"></param>
        public void DelUpdateAction(UnityAction action)
        {
            updateUnityAction -= action;
            updateUnityActionList.Remove(action);
        }

        /// <summary>
        /// 删除所有更新事件
        /// </summary>
        public void DelAllUpdateActions()
        {
            foreach (var unityAction in updateUnityActionList)
                DelUpdateAction(unityAction);
        }
    }
}
