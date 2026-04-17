// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/12 17:04
// @version: 1.0
// @description:
// *****************************************************************************

using HoshiVerseFramework.Base;
using UnityEngine;
using Yumihoshi.VFX.Entites.PPV;

namespace Yumihoshi.Singletons
{
    public class VFXManager : Singleton<VFXManager>
    {
        /// <summary>
        /// 后处理体积
        /// </summary>
        public PPV PostProcessingVolume { get; private set; }

        private void Start()
        {
            PostProcessingVolume =
                GameObject.FindWithTag("PPV").GetComponent<PPV>();
            if (!PostProcessingVolume)
                Debug.LogError("[VFX] 未找到后处理体积");
        }
    }
}
