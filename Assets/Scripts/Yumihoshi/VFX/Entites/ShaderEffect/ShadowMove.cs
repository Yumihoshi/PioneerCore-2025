// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/18 16:45
// @version: 1.0
// @description:
// *****************************************************************************

using UnityEngine;
using Yumihoshi.VFX.Entites.Base;

namespace Yumihoshi.VFX.Entites.ShaderEffect
{
    public class ShadowMove : BaseShaderEffect
    {
        private static readonly int
            RippleCenterID = Shader.PropertyToID("_RippleCenter"),
            RippleCountID = Shader.PropertyToID("_RippleCount"),
            RippleStrengthID = Shader.PropertyToID("_RippleStrength"),
            RippleSpeedID = Shader.PropertyToID("_RippleSpeed"),
            UVScaleID = Shader.PropertyToID("_UVScale"),
            UVOffsetID = Shader.PropertyToID("_UVOffset"),
            EnableID = Shader.PropertyToID("_Enable");

        public ShadowMove() : base("ShadowMove")
        {
        }

        /// <summary>
        /// 启用或禁用波纹效果
        /// </summary>
        public bool Enable
        {
            get => mat.GetInt(EnableID) == 1;
            set => mat.SetInt(EnableID, value ? 1 : 0);
        }

        /// <summary>
        /// 波纹中心uv，默认值为(0.5, 0.5)
        /// </summary>
        public Vector2 RippleCenter
        {
            get => mat.GetVector(RippleCenterID);
            set => mat.SetVector(RippleCenterID, value);
        }

        /// <summary>
        /// 波纹数量，默认值为4
        /// </summary>
        public float RippleCount
        {
            get => mat.GetFloat(RippleCountID);
            set => mat.SetFloat(RippleCountID, value);
        }

        /// <summary>
        /// 波纹强度，默认值1，范围为0到1
        /// </summary>
        public float RippleStrength
        {
            get => mat.GetFloat(RippleStrengthID);
            set
            {
                float modifiedValue = Mathf.Clamp01(value);
                if (!Mathf.Approximately(modifiedValue, value))
                    Debug.LogWarning("RippleStrength值超出范围，已自动调整为0到1之间。");
                mat.SetFloat(RippleStrengthID, value);
            }
        }

        /// <summary>
        /// 应用波纹后的uv缩放处理，默认值(1, 0.87)
        /// </summary>
        public Vector2 UVScale
        {
            get => mat.GetVector(UVScaleID);
            set => mat.SetVector(UVScaleID, value);
        }

        /// <summary>
        /// 应用波纹后的uv偏移处理
        /// </summary>
        public Vector2 UVOffset
        {
            get => mat.GetVector(UVOffsetID);
            set => mat.SetVector(UVOffsetID, value);
        }

        /// <summary>
        /// 波纹速度，默认值0.7
        /// </summary>
        public float RippleSpeed
        {
            get => mat.GetFloat(RippleSpeedID);
            set => mat.SetFloat(RippleSpeedID, value);
        }
    }
}
