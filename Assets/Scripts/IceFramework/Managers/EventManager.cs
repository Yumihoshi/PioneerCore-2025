using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace IceFramework.EventManager
{
    public interface IEventInfo
    {
    }

    public class EventInfo : IEventInfo
    {
        public UnityAction actions;

        public EventInfo(UnityAction action)
        {
            actions += action;
        }
    }

    public class EventInfo<T> : IEventInfo
    {
        public UnityAction<T> actions;

        public EventInfo(UnityAction<T> action)
        {
            actions += action;
        }
    }

    public class EventInfo<T, K> : IEventInfo
    {
        public UnityAction<T, K> actions;

        public EventInfo(UnityAction<T, K> action)
        {
            actions += action;
        }
    }

    public class EventFuncInfo<T, K> : IEventInfo
    {
        public Func<T, K> actions;

        public EventFuncInfo(Func<T, K> action)
        {
            actions += action;
        }
    }

    public class EventInfo<T, K, M> : IEventInfo
    {
        public UnityAction<T, K, M> actions;

        public EventInfo(UnityAction<T, K, M> action)
        {
            actions += action;
        }
    }

    public class EventInfo<T, K, M, L> : IEventInfo
    {
        public UnityAction<T, K, M, L> actions;

        public EventInfo(UnityAction<T, K, M, L> action)
        {
            actions += action;
        }
    }


    public class EventManager : SingletonBace<EventManager>
    {
        private readonly Dictionary<string, IEventInfo> eventDic = new();

        /// <summary>
        /// 添加事件监听
        /// </summary>
        /// <param ListenerName="eventName">被监听的事件名称</param>
        /// <param ListenerName="action">触发的事件行为</param>
        public void AddEventListener(string eventName, UnityAction action)
        {
            if (eventDic.ContainsKey(eventName))
                (eventDic[eventName] as EventInfo).actions += action;
            else
                eventDic.Add(eventName, new EventInfo(action));
        }

        public void AddEventListener<T>(string eventName, UnityAction<T> action)
        {
            if (eventDic.ContainsKey(eventName))
                (eventDic[eventName] as EventInfo<T>).actions += action;
            else
                eventDic.Add(eventName, new EventInfo<T>(action));
        }

        public void AddEventListener<T, K>(string eventName,
            UnityAction<T, K> action)
        {
            if (eventDic.ContainsKey(eventName))
                (eventDic[eventName] as EventInfo<T, K>).actions += action;
            else
                eventDic.Add(eventName, new EventInfo<T, K>(action));
        }

        public void AddEventListener<T, K>(string eventName, Func<T, K> func)
        {
            if (eventDic.ContainsKey(eventName))
                (eventDic[eventName] as EventFuncInfo<T, K>).actions += func;
            else
                eventDic.Add(eventName, new EventFuncInfo<T, K>(func));
        }

        public void AddEventListener<T, K, M>(string eventName,
            UnityAction<T, K, M> action)
        {
            if (eventDic.ContainsKey(eventName))
                (eventDic[eventName] as EventInfo<T, K, M>).actions += action;
            else
                eventDic.Add(eventName, new EventInfo<T, K, M>(action));
        }


        /// <summary>
        /// 事件触发器
        /// </summary>
        /// <param ListenerName="eventName">触发的被监听事件名称</param>
        public void EventTrigger(string eventName)
        {
            if (!eventDic.ContainsKey(eventName)) return;
            (eventDic[eventName] as EventInfo).actions.Invoke();
        }

        public void EventTrigger<T>(string eventName, T info)
        {
            if (!eventDic.ContainsKey(eventName)) return;
            (eventDic[eventName] as EventInfo<T>).actions.Invoke(info);
        }

        public void EventTrigger<T, K>(string eventName, T info, K info1)
        {
            if (!eventDic.ContainsKey(eventName)) return;
            (eventDic[eventName] as EventInfo<T, K>).actions
                .Invoke(info, info1);
        }

        public void EventTrigger<T, K>(string eventName, T info, out K res)
        {
            if (!eventDic.ContainsKey(eventName))
            {
                res = default;
                return;
            }

            res = (eventDic[eventName] as EventFuncInfo<T, K>).actions
                .Invoke(info);
        }

        public void EventTrigger<T, K, M>(string eventName, T info1, K info2,
            M info3)
        {
            if (!eventDic.ContainsKey(eventName)) return;
            (eventDic[eventName] as EventInfo<T, K, M>).actions.Invoke(info1,
                info2, info3);
        }

        public void EventTrigger<T, K, M, L>(string eventName, T info1, K info2,
            M info3, L info4)
        {
            if (!eventDic.ContainsKey(eventName)) return;
            (eventDic[eventName] as EventInfo<T, K, M, L>).actions.Invoke(info1,
                info2, info3, info4);
        }

        /// <summary>
        /// 移除事件监听
        /// </summary>
        /// <param ListenerName="eventName">被监听的事件名称</param>
        /// <param ListenerName="action">触发的事件行为</param>
        public void RemoveEventListener(string eventName, UnityAction action)
        {
            if (!eventDic.ContainsKey(eventName)) return;
            (eventDic[eventName] as EventInfo).actions -= action;
        }

        public void RemoveEventListener<T>(string eventName,
            UnityAction<T> action)
        {
            if (!eventDic.ContainsKey(eventName)) return;
            (eventDic[eventName] as EventInfo<T>).actions -= action;
        }

        public void RemoveEventListener<T, K>(string eventName,
            UnityAction<T, K> func)
        {
            if (!eventDic.ContainsKey(eventName)) return;
            (eventDic[eventName] as EventInfo<T, K>).actions -= func;
        }

        public void RemoveEventListener<T, K>(string eventName, Func<T, K> func)
        {
            if (!eventDic.ContainsKey(eventName)) return;
            (eventDic[eventName] as EventFuncInfo<T, K>).actions -= func;
        }

        public void RemoveEventListener<T, K, M>(string eventName,
            UnityAction<T, K, M> action)
        {
            if (!eventDic.ContainsKey(eventName)) return;
            (eventDic[eventName] as EventInfo<T, K, M>).actions -= action;
        }

        /// <summary>
        /// 移除所有事件监听
        /// </summary>
        public void RemoveAllListener()
        {
            eventDic.Clear();
        }
    }
}
