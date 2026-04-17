// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/12 14:04
// @version: 1.0
// @description:
// *****************************************************************************

using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Yumihoshi.UI.GetObjTip
{
    public class TMPTip : MonoBehaviour
    {
        private Color _clearColor;
        private Color _initColor;
        private TextMeshProUGUI _tmp;

        private void Awake()
        {
            _tmp = GetComponent<TextMeshProUGUI>();
            _initColor = _tmp.color;
            _clearColor =
                new Color(_initColor.r, _initColor.g, _initColor.b, 0);
            _tmp.color = _clearColor;
        }

        private void OnDestroy()
        {
            _tmp.DOKill();
            StopAllCoroutines();
        }

        /// <summary>
        /// 渐入显示
        /// </summary>
        /// <param name="duration">渐变时间</param>
        /// <param name="showTime">显示时间</param>
        /// <param name="fadeInComplete">渐入完成回调</param>
        /// <param name="fadeOutComplete">渐出完成回调</param>
        public void FadeIn(float duration = 0.5f, float showTime = 2f,
            Action fadeInComplete = null, Action fadeOutComplete = null)
        {
            _tmp.color = _clearColor;
            _tmp.DOColor(_initColor, duration).OnComplete(() =>
            {
                fadeInComplete?.Invoke();
                StartCoroutine(
                    DelayFadeOut(duration, showTime, fadeOutComplete));
            });
        }

        private IEnumerator DelayFadeOut(float duration = 0.5f,
            float showTime = 2f, Action onComplete = null)
        {
            yield return new WaitForSeconds(showTime);
            FadeOut(duration, onComplete);
        }

        /// <summary>
        /// 渐入显示，并更新默认颜色
        /// </summary>
        /// <param name="color"></param>
        /// <param name="duration"></param>
        /// <param name="onComplete"></param>
        public void FadeIn(Color color, float duration = 0.5f,
            Action onComplete = null)
        {
            _initColor = color;
            _clearColor =
                new Color(_initColor.r, _initColor.g, _initColor.b, 0);
            _tmp.color = _clearColor;
            _tmp.DOColor(color, duration).OnComplete(() =>
            {
                onComplete?.Invoke();
                StopAllCoroutines();
            });
        }

        /// <summary>
        /// 渐出隐藏
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="onComplete"></param>
        public void FadeOut(float duration = 0.5f, Action onComplete = null)
        {
            _tmp.color = _initColor;
            _tmp.DOColor(_clearColor, duration).OnComplete(() =>
            {
                onComplete?.Invoke();
                Destroy(gameObject);
            });
        }

        /// <summary>
        /// 停止渐变
        /// </summary>
        /// <param name="complete">是否完成渐变</param>
        public void StopFade(bool complete = true)
        {
            _tmp.DOComplete(complete);
        }

        public void SetText(string text)
        {
            _tmp.text = text;
        }
    }
}
