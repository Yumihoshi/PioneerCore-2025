// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/09 15:04
// @version: 1.0
// @description:
// *****************************************************************************

using HoshiVerseFramework.Base;
using HoshiVerseFramework.Components.Factory;
using HoshiVerseFramework.Configs;
using UnityEngine;

namespace Yumihoshi.Singletons
{
    public class VFXFactory : Singleton<VFXFactory>
    {
        private const string VFX_CONFIG_PATH = "VFXPoolConfig";
        private VFXPoolConfigList _configList;
        public FactoryComponent Factory { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            _configList = Resources.Load<VFXPoolConfigList>(VFX_CONFIG_PATH);
            if (!_configList)
            {
                Debug.LogError("[VFX] 特效工厂单例：未找到特效对象池列表配置文件");
                return;
            }

            Factory = new FactoryComponent(_configList);
        }
    }
}
