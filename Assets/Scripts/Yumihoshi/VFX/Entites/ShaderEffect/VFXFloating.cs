// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/13 11:04
// @version: 1.0
// @description:
// *****************************************************************************

using UnityEngine;
using Yumihoshi.VFX.Entites.Base;

namespace Yumihoshi.VFX.Entites.ShaderEffect
{
    public class VFXFloating : BaseShaderEffect
    {
        private static readonly int EnableID = Shader.PropertyToID("_Enable");
        private static readonly int SpeedID = Shader.PropertyToID("_Speed");
        private static readonly int RangeID = Shader.PropertyToID("_Range");

        public VFXFloating() : base("Floating")
        {
        }

        /// <summary>
        /// 启用或禁用浮动效果
        /// </summary>
        /// <param name="enable">是否启用浮动效果</param>
        public void SetEnable(bool enable = true)
        {
            mat.SetInt(EnableID, enable ? 1 : 0);
        }

        /// <summary>
        /// 设置浮动效果的速度
        /// </summary>
        /// <param name="speed">浮动效果的速度</param>
        public void SetSpeed(float speed = 3.5f)
        {
            mat.SetFloat(SpeedID, speed);
        }

        /// <summary>
        /// <para>设置浮动效果的范围</para>
        /// </summary>
        /// <param name="range">浮动效果范围，参数范围：[(-1, -1), (1, 1)]，默认值(0, 0.5)</param>
        public void SetRange(Vector2 range)
        {
            var modifiedRange = new Vector2(Mathf.Clamp(range.x, -1, 1),
                Mathf.Clamp(range.y, -1, 1));
            if (modifiedRange != range)
                Debug.LogWarning("[VFX] 浮动范围超出，已修改为：" + modifiedRange);
            mat.SetVector(RangeID, modifiedRange);
        }
    }
}
