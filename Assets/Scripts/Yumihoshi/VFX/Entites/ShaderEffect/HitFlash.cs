// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/18 15:04
// @version: 1.0
// @description:
// *****************************************************************************

using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Yumihoshi.VFX.Entites.Base;

namespace Yumihoshi.VFX.Entites.ShaderEffect
{
    public class HitFlash : BaseShaderEffect
    {
        private static readonly int
            ColorID = Shader.PropertyToID("_Color"),
            IntensifyID = Shader.PropertyToID("_Intensify"),
            ProgressID = Shader.PropertyToID("_Progress"),
            EnableID = Shader.PropertyToID("_Enable");

        public HitFlash() : base("HitFlash")
        {
        }

        /// <summary>
        /// 启用或禁用受击闪烁效果
        /// </summary>
        public bool Enable
        {
            get => mat.GetInt(EnableID) == 1;
            set => mat.SetInt(EnableID, value ? 1 : 0);
        }

        /// <summary>
        /// 受击闪烁颜色，默认为浅红
        /// </summary>
        public Color HitColor
        {
            get => mat.GetColor(ColorID);
            set => mat.SetColor(ColorID, value);
        }

        /// <summary>
        /// 受击闪烁强度，范围为0到1，默认为1
        /// </summary>
        public float Intensify
        {
            get => mat.GetFloat(IntensifyID);
            set
            {
                float modifiedValue = Mathf.Clamp01(value);
                if (!Mathf.Approximately(modifiedValue, value))
                    Debug.LogWarning("Intensify值超出范围，已自动调整为0到1之间。");
                mat.SetFloat(IntensifyID, modifiedValue);
            }
        }

        /// <summary>
        /// 受击闪烁进度，范围为0到1，默认为0
        /// </summary>
        public float Progress
        {
            get => mat.GetFloat(ProgressID);
            set
            {
                float modifiedValue = Mathf.Clamp01(value);
                if (!Mathf.Approximately(modifiedValue, value))
                    Debug.LogWarning("Progress值超出范围，已自动调整为0到1之间。");
                mat.SetFloat(ProgressID, modifiedValue);
            }
        }

        /// <summary>
        /// 执行受击闪烁效果，默认颜色为浅红
        /// </summary>
        /// <param name="mono">要应用闪烁效果的挂载脚本</param>
        /// <param name="duration">闪烁持续时间，默认为0.5秒</param>
        /// <param name="intensify">闪烁强度，范围为0到1，默认为1</param>
        /// <param name="onComplete">完成回调</param>
        /// <typeparam name="T">当前脚本</typeparam>
        public void Flash<T>(T mono, float duration = 0.5f,
            float intensify = 1f, Action onComplete = null)
            where T : MonoBehaviour
        {
            var flashColor = new Color(194 / 255f, 60 / 255f, 60 / 255f);
            mono.StartCoroutine(FlashCoroutine(flashColor, duration,
                intensify, onComplete));
        }

        /// <summary>
        /// 执行受击闪烁效果，指定闪烁颜色
        /// </summary>
        /// <param name="mono">要应用闪烁效果的挂载脚本</param>
        /// <param name="flashColor">闪烁颜色</param>
        /// <param name="duration">闪烁持续时间，默认为0.5秒</param>
        /// <param name="intensify">闪烁强度，范围为0到1，默认为1</param>
        /// <param name="onComplete">完成回调</param>
        /// <typeparam name="T">当前脚本</typeparam>
        public void Flash<T>(T mono, Color flashColor, float duration = 0.5f,
            float intensify = 1f, Action onComplete = null)
            where T : MonoBehaviour
        {
            mono.StartCoroutine(FlashCoroutine(flashColor, duration,
                intensify, onComplete));
        }

        private IEnumerator FlashCoroutine(Color flashColor, float duration,
            float intensify, Action onComplete)
        {
            // 闪白
            HitColor = Color.white;
            Progress = 1f;
            yield return new WaitForSeconds(0.1f);
            // 闪烁
            HitColor = flashColor;
            DOTween.To(() => Progress, x => Progress = x, 0f, duration)
                .SetEase(Ease.Linear).OnComplete(() => onComplete?.Invoke());
        }
    }
}
