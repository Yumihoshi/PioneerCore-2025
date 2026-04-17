// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/05 21:04
// @version: 1.0
// @description:
// *****************************************************************************

using System;
using HoshiVerseFramework.Components.Factory;
using UnityEngine;

namespace HoshiVerseFramework.Configs
{
    [Serializable]
    public class VFXPoolConfig
    {
        [Header("特效对象池配置")] public GameObject prefab;
        public VFXType vfxType;
        public int defaultCapacity = 20;
        public int maxCapacity = 100;
        public bool collectionChecks = true;
        [Header("自动释放配置")] public bool autoRelease = true;
        public float lifeTime = 5f;
    }
}
