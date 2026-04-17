// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/12 14:04
// @version: 1.0
// @description:
// *****************************************************************************

using System.Collections.Generic;
using UnityEngine;

namespace Yumihoshi.UI.GetObjTip
{
    public class TipHandler : MonoBehaviour
    {
        [SerializeField] private GameObject tmpTip;

        private readonly Queue<TMPTip> _tipShowQueue = new();
        private Transform _panel;

        private void Start()
        {
            _panel = transform.Find("Panel_Tip");
        }

        /// <summary>
        /// 显示物品拾取提示
        /// </summary>
        /// <param name="text"></param>
        /// <param name="duration">渐变动画时间</param>
        /// <param name="showTime">显示时间</param>
        public void ShowTip(string text, float duration = 0.5f,
            float showTime = 2f)
        {
            // 如果超过5个，就把最早的那个销毁
            if (_tipShowQueue.Count >= 5)
                Destroy(_tipShowQueue.Dequeue().gameObject);

            // 实例化一个新的提示框
            var newTip = Instantiate(tmpTip, _panel).GetComponent<TMPTip>();
            _tipShowQueue.Enqueue(newTip);
            newTip.gameObject.SetActive(true);
            newTip.SetText(text);
            newTip.FadeIn(duration, showTime, null,
                () => { _tipShowQueue.Dequeue(); });
        }
    }
}
