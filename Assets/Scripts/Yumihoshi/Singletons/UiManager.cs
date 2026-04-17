// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/12 14:04
// @version: 1.0
// @description:
// *****************************************************************************

using HoshiVerseFramework.Base;
using UnityEngine;
using Yumihoshi.UI.GetObjTip;

namespace Yumihoshi.Singletons
{
    public class UiManager : Singleton<UiManager>
    {
        public TipHandler ObjTip { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            ObjTip = GameObject.FindWithTag("GetObjTipCanvas")
                .GetComponent<TipHandler>();
            if (!ObjTip) Debug.LogError("[UiManager] 未找到获取物品提示Canvas");
        }
    }
}
