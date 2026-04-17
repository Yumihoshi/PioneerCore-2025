// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/12 17:04
// @version: 1.0
// @description:
// *****************************************************************************

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Yumihoshi.VFX.Entites.PPV
{
    public class VignetteEffect
    {
        private readonly Volume _volume;

        public VignetteEffect(Volume volume)
        {
            _volume = volume;
        }

        /// <summary>
        /// 设置是否启用暗角
        /// </summary>
        /// <param name="enable"></param>
        public void SetEnable(bool enable = true)
        {
            _volume.sharedProfile.TryGet(out Vignette vignette);
            vignette.active = enable;
        }

        /// <summary>
        /// 设置暗角强度
        /// </summary>
        /// <param name="intensity">暗角强度，范围0-1，越大越明显</param>
        public void SetIntensify(float intensity)
        {
            _volume.sharedProfile.TryGet(out Vignette vignette);
            float modifiedIntensity = Mathf.Clamp01(intensity);
            vignette.intensity.value = modifiedIntensity;
        }

        /// <summary>
        /// 设置暗角颜色
        /// </summary>
        /// <param name="color"></param>
        public void SetColor(Color color)
        {
            _volume.sharedProfile.TryGet(out Vignette vignette);
            vignette.color.value = color;
        }

        /// <summary>
        /// 设置暗角平滑度
        /// </summary>
        /// <param name="smoothness">暗角平滑度，范围0-1</param>
        public void SetSmoothness(float smoothness = 0.2f)
        {
            _volume.sharedProfile.TryGet(out Vignette vignette);
            float modifiedSmoothness = Mathf.Clamp01(smoothness);
            vignette.smoothness.value = modifiedSmoothness;
        }

        /// <summary>
        /// 设置暗角是否为圆角
        /// </summary>
        /// <param name="rounded"></param>
        public void SetRounded(bool rounded = true)
        {
            _volume.sharedProfile.TryGet(out Vignette vignette);
            vignette.rounded.value = rounded;
        }
    }
}
