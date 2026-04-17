// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/16 22:04
// @version: 1.0
// @description:
// *****************************************************************************

using UnityEngine;
using Yumihoshi.VFX.Entites.Base;

namespace Yumihoshi.VFX.Entites.ShaderEffect
{
    public class OutlineEffect : BaseShaderEffect
    {
        // 精灵图轮廓相关参数ID
        private static readonly int
            OutlineOffsetID = Shader.PropertyToID("_OutlineOffset"),
            OutlineColorID = Shader.PropertyToID("_OutlineColor"),
            EnableNoiseID = Shader.PropertyToID("_EnableNoise"),
            NoiseScaleID = Shader.PropertyToID("_NoiseScale"),
            NoiseOffsetID = Shader.PropertyToID("_NoiseOffset"),
            NoiseSpeedID = Shader.PropertyToID("_NoiseSpeed"),
            IsDirectionUpID = Shader.PropertyToID("_IsDirectionUp"),
            NoiseStepOneID = Shader.PropertyToID("_NoiseStepOne"),
            NoiseStepTwoID = Shader.PropertyToID("_NoiseStepTwo"),
            EnableID = Shader.PropertyToID("_Enable");

        public OutlineEffect() : base("SpriteOutline")
        {
        }

        /// <summary>
        /// 是否启用轮廓效果
        /// </summary>
        public bool Enable
        {
            get => mat.GetInt(EnableID) == 1;
            set => mat.SetInt(EnableID, value ? 1 : 0);
        }

        /// <summary>
        /// <para>轮廓偏移量</para>
        /// <para>默认值：0.02，范围：[0, 1]</para>
        /// <para>修改后立即应用</para>
        /// </summary>
        public float OutlineOffset
        {
            get => mat.GetFloat(OutlineOffsetID);
            set
            {
                // 应用偏移量
                float modifiedOffset = Mathf.Clamp01(value);
                if (!Mathf.Approximately(modifiedOffset, value))
                    Debug.LogWarning(
                        $"[VFX] 轮廓偏移量{value}超出范围[0, 1]，已自动调整为合法范围{modifiedOffset}");
                mat.SetFloat(OutlineOffsetID, modifiedOffset);
            }
        }

        /// <summary>
        /// <para>轮廓颜色</para>
        /// <para>默认值：(24/255f, 1, 0, 0)</para>
        /// <para>注意：a通道会重置为0</para>
        /// <para>修改后立即应用</para>
        /// </summary>
        public Color OutlineColor
        {
            get => mat.GetColor(OutlineColorID);
            set
            {
                // 应用颜色
                Color modifiedColor = value;
                modifiedColor.a = 0f;
                mat.SetColor(OutlineColorID, modifiedColor);
            }
        }

        /// <summary>
        /// <para>是否启用轮廓噪声</para>
        /// <para>默认值：false</para>
        /// <para>修改后立即应用</para>
        /// </summary>
        public bool EnableNoise
        {
            get => mat.GetInt(EnableNoiseID) == 1;
            set => mat.SetInt(EnableNoiseID, value ? 1 : 0);
        }

        /// <summary>
        /// <para>轮廓噪声缩放（仅在EnableNoise为True时生效）</para>
        /// <para>默认值：474</para>
        /// <para>作用：控制轮廓动画中单个透明块的大小</para>
        /// </summary>
        public float NoiseScale
        {
            get => mat.GetFloat(NoiseScaleID);
            set => mat.SetFloat(NoiseScaleID, value);
        }

        /// <summary>
        /// <para>轮廓噪声偏移（仅在EnableNoise为True时生效）</para>
        /// <para>默认值：2.02</para>
        /// </summary>
        public float NoiseOffset
        {
            get => mat.GetFloat(NoiseOffsetID);
            set => mat.SetFloat(NoiseOffsetID, value);
        }

        /// <summary>
        /// <para>轮廓噪声速度（仅在EnableNoise为True时生效）</para>
        /// <para>默认值：0.1</para>
        /// <para>作用：轮廓动画速度</para>
        /// </summary>
        public float NoiseSpeed
        {
            get => mat.GetFloat(NoiseSpeedID);
            set => mat.SetFloat(NoiseSpeedID, value);
        }

        /// <summary>
        /// <para>轮廓动画方向（True为向上，False为向下）（仅在EnableNoise为True时生效）</para>
        /// <para>默认值：True</para>
        /// </summary>
        public bool IsDirectionUp
        {
            get => mat.GetInt(IsDirectionUpID) == 1;
            set => mat.SetInt(IsDirectionUpID, value ? 1 : 0);
        }

        /// <summary>
        /// <para>噪声过滤阈值1（仅在EnableNoise为True时生效）</para>
        /// <para>默认值：0.3</para>
        /// </summary>
        public float NoiseStepOne
        {
            get => mat.GetFloat(NoiseStepOneID);
            set => mat.SetFloat(NoiseStepOneID, value);
        }

        /// <summary>
        /// <para>噪声过滤阈值2（仅在EnableNoise为True时生效）</para>
        /// <para>默认值：0.5</para>
        /// </summary>
        public float NoiseStepTwo
        {
            get => mat.GetFloat(NoiseStepTwoID);
            set => mat.SetFloat(NoiseStepTwoID, value);
        }
    }
}
