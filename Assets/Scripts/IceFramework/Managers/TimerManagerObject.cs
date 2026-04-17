using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

//计时器管理器
//分为调用端和存储端
//只处理计时器的更新与监听

namespace IceFramework.TimerManager
{
    public class TimerManagerObject : SerializedMonoBehaviour
    {
        [Title("计时器字典")] [DictionaryDrawerSettings] [ShowInInspector]
        public static Dictionary<string, TimerInfo> Dic = new(); //计时器字典

        private void Update() //此处更新每个计时器
        {
            foreach (var (key, value) in Dic)
                value.timer += value.withScale
                    ? Time.deltaTime
                    : Time.unscaledDeltaTime;
        }

        [HideReferenceObjectPicker]
        public class TimerInfo //计时器数据
        {
            public float cd;
            public float timer;
            public bool withScale;

            public TimerInfo(float timer, float cd, bool withScale)
            {
                this.timer = timer;
                this.cd = cd;
                this.withScale = withScale;
            }

            public void Reset()
            {
                timer = 0;
            }
        }

        #region PrivateMethod

        [Button("Add")]
        [ButtonGroup]
        public string Add(string key = "NewTimer", float value = 0,
            bool withScale = false)
        {
            string newKey = Check(0);
            Debug.Log(newKey);
            Dic.Add(newKey, new TimerInfo(value, value, withScale));
            if (value == 0) Debug.LogWarning("计时器" + key + "未设置冷却");
            return newKey;

            string Check(int index)
            {
                if (Dic.TryGetValue(key + index, out TimerInfo i))
                    return Check(index + 1);
                key += index;
                return key;
            }
        }

        [Button("Reset")]
        [ButtonGroup]
        private void AllReset()
        {
            foreach (var (key, value) in Dic) value.Reset();
        }

        private void Reset(string key)
        {
            if (Dic.TryGetValue(key, out TimerInfo i)) i.Reset();
        }

        [Button("Clear")]
        [ButtonGroup]
        private void AllClear()
        {
            Dic.Clear();
        }

        private void Clear(string key)
        {
            if (Dic.TryGetValue(key, out TimerInfo i)) Dic.Remove(key);
        }

        #endregion
    }

    public class TimerManager : SingletonBace<TimerManager>
    {
        private static GameObject TimerManagerObject;

        private static readonly Dictionary<string, TimerManagerObject.TimerInfo>
            Dic = IceFramework.TimerManager.TimerManagerObject.Dic;

        #region Method

        /// <summary>
        /// 添加一个计时器，会对key进行重命名以防止计时器重复
        /// </summary>
        /// <param ListenerName="key">键名</param>
        /// <param ListenerName="value">冷却</param>
        /// <param ListenerName="isOver">计时器超出冷却？</param>
        /// <param ListenerName="withScale">计时器与游戏内时间流逝速度相关？</param>
        public string Add(string key = "NewTimer", float value = 0,
            bool isOver = false, bool withScale = false)
        {
            string newKey = Check(0);
            Dic.Add(newKey,
                new TimerManagerObject.TimerInfo(value, value, withScale));
            SetTimer(newKey, isOver ? value + 1 : 0);
            return newKey;

            string Check(int index)
            {
                if (Dic.TryGetValue(key + index,
                        out TimerManagerObject.TimerInfo i))
                    return Check(index + 1);
                key += index;
                return key;
            }
        }

        public void Reset(string key)
        {
            if (Dic.TryGetValue(key, out TimerManagerObject.TimerInfo i))
            {
                i.Reset();
                return;
            }

            Debug.LogWarning("计时器不存在");
        }

        public void Remove(string key)
        {
            if (Dic.Remove(key)) return;
            Debug.LogWarning("计时器不存在");
        }

        public float GetCd(string key)
        {
            if (Dic.TryGetValue(key, out TimerManagerObject.TimerInfo i))
                return i.cd;
            throw new IceFrameworkException("计时器不存在");
        }

        public void SetCd(string key, float value)
        {
            if (Dic.TryGetValue(key, out TimerManagerObject.TimerInfo i))
            {
                i.cd = value;
                return;
            }

            throw new IceFrameworkException("计时器不存在");
        }

        public float GetTimer(string key)
        {
            if (Dic.TryGetValue(key, out TimerManagerObject.TimerInfo i))
                return i.timer;
            throw new IceFrameworkException("计时器不存在");
        }

        public void SetTimer(string key, float value)
        {
            if (Dic.TryGetValue(key, out TimerManagerObject.TimerInfo i))
            {
                i.timer = value;
                return;
            }

            throw new IceFrameworkException("计时器不存在");
        }

        /// <summary>
        /// 计时器超出冷却
        /// </summary>
        /// <returns>如果计时超过冷却返回true</returns>
        public bool TimerOver(string key)
        {
            return GetTimer(key) >= GetCd(key);
        }

        /// <summary>
        /// 计时器未超出冷却
        /// </summary>
        /// <returns>如果计时未超过冷却返回true</returns>
        public bool TimerNotOver(string key)
        {
            return GetTimer(key) < GetCd(key);
        }

        /// <summary>
        /// 计时器单位化
        /// </summary>
        public float UnitizationTimer(string key)
        {
            if (GetCd(key) > 0) return GetTimer(key) / GetCd(key);
            throw new IceFrameworkException("计时器单位化错误");
        }

        /// <summary>
        /// 检查计时器是否存在
        /// </summary>
        public bool ContainsTimer(string key)
        {
            return Dic.ContainsKey(key);
        }

        #endregion
    }
}
