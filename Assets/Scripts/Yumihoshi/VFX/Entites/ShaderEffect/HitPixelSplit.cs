// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/19 12:58
// @version: 1.0
// @description:
// *****************************************************************************

using DG.Tweening;
using UnityEngine;
using Yumihoshi.VFX.Entites.Base;

namespace Yumihoshi.VFX.Entites.ShaderEffect
{
    public class HitPixelSplit : BaseShaderEffect
    {
        private static readonly int
            NumBlocksID = Shader.PropertyToID("_NumBlocks"),
            SpeedID = Shader.PropertyToID("_Speed"),
            OffsetID = Shader.PropertyToID("_Offset"),
            NoiseScaleID = Shader.PropertyToID("_NoiseScale"),
            EnableID = Shader.PropertyToID("_Enable");

        public HitPixelSplit() : base("HitPixelSplit")
        {
        }

        /// <summary>
        /// 是否启用受击分离效果
        /// </summary>
        public bool Enable
        {
            get => mat.GetInt(EnableID) == 1;
            set => mat.SetInt(EnableID, value ? 1 : 0);
        }

        /// <summary>
        /// 分块数量
        /// </summary>
        public int NumBlocks
        {
            get => mat.GetInt(NumBlocksID);
            set => mat.SetInt(NumBlocksID, value);
        }

        /// <summary>
        /// 晃动速度，默认值1f
        /// </summary>
        public float Speed
        {
            get => mat.GetFloat(SpeedID);
            set => mat.SetFloat(SpeedID, value);
        }

        /// <summary>
        /// 偏移，默认值(-0.053f, 0)
        /// </summary>
        public Vector2 Offset
        {
            get => mat.GetVector(OffsetID);
            set => mat.SetVector(OffsetID, value);
        }

        /// <summary>
        /// 噪声缩放，默认值100f
        /// </summary>
        public float NoiseScale
        {
            get => mat.GetFloat(NoiseScaleID);
            set => mat.SetFloat(NoiseScaleID, value);
        }

        /// <summary>
        /// 像素分离
        /// </summary>
        /// <param name="duration">动画时间</param>
        public void PixelSplit(float duration = 1f)
        {
            NoiseScale = 0f;
            DOTween.To(() => NoiseScale, x => NoiseScale = x, 100f, duration);
        }

        /// <summary>
        /// 像素复原
        /// </summary>
        /// <param name="duration"></param>
        public void PixelRecover(float duration = 1f)
        {
            NoiseScale = 100f;
            DOTween.To(() => NoiseScale, x => NoiseScale = x, 0f, duration);
        }
    }
}
