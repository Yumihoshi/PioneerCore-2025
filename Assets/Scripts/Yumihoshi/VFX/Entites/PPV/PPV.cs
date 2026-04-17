// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/12 17:04
// @version: 1.0
// @description:
// *****************************************************************************

using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace Yumihoshi.VFX.Entites.PPV
{
    public class PPV : MonoBehaviour
    {
        private Volume _volume;
        public VignetteEffect Vignette { get; private set; }

        private void Awake()
        {
            _volume = GetComponent<Volume>();
            if (!_volume)
            {
                Debug.LogError("[VFX] 后处理体积组件不存在");
                return;
            }

            Vignette = new VignetteEffect(_volume);
        }

        /// <summary>
        /// 暗角闪烁
        /// </summary>
        /// <param name="color">闪烁颜色</param>
        /// <param name="interval">闪烁间隔</param>
        /// <param name="count">闪烁次数</param>
        public void VignetteFlash(Color color, float interval = 0.1f,
            int count = 5)
        {
            StartCoroutine(FlashCoroutine(color, interval, count));
        }

        private IEnumerator FlashCoroutine(Color color, float interval,
            int count)
        {
            Vignette.SetEnable();
            for (var i = 0; i < count; i++)
            {
                Vignette.SetColor(color);
                Vignette.SetIntensify(0.5f);
                yield return new WaitForSeconds(interval);
                Vignette.SetIntensify(0f);
                yield return new WaitForSeconds(interval);
            }

            Vignette.SetEnable(false);
        }
    }
}
