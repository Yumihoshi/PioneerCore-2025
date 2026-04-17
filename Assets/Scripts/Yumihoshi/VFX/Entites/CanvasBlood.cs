// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/18 17:59
// @version: 1.0
// @description:
// *****************************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Yumihoshi.VFX.Entites
{
    public class CanvasBlood : MonoBehaviour
    {
        [Header("屏幕血迹配置")] [SerializeField]
        private List<GameObject> bloodPanelList;

        private void Awake()
        {
            // 隐藏所有屏幕血迹
            foreach (GameObject go in bloodPanelList)
                go.SetActive(false);
        }

        /// <summary>
        /// 显示屏幕血迹
        /// </summary>
        /// <param name="stage">阶段数，一阶段为两角，二阶段为全屏</param>
        public void ShowBlood(int stage = 1)
        {
            switch (stage)
            {
                case 1:
                    bloodPanelList[1].SetActive(true);
                    bloodPanelList[2].SetActive(true);
                    break;
                case 2:
                    ShowBlood();
                    bloodPanelList[0].SetActive(true);
                    break;
                default:
                    Debug.LogError("[VFX] CanvasBlood: 不存在的阶段数");
                    return;
            }
        }


        /// <summary>
        /// 屏幕血迹淡出
        /// </summary>
        /// <param name="fadeTime">淡出时间</param>
        /// <param name="onFadeComplete">淡出回调</param>
        public void FadeOutBlood(float fadeTime = 0.5f,
            Action onFadeComplete = null)
        {
            StartCoroutine(FadeOut(fadeTime, onFadeComplete));
        }

        private IEnumerator FadeOut(float fadeTime = 0.5f,
            Action onFadeComplete = null)
        {
            var count = 0;
            var nowCount = 0;
            foreach (GameObject go in bloodPanelList.Where(go =>
                         go.activeInHierarchy))
            {
                Image[] imgs = go.GetComponentsInChildren<Image>();
                foreach (Image img in imgs)
                {
                    count++;
                    img.DOColor(new Color(1, 1, 1, 0), fadeTime)
                        .OnComplete(() => { nowCount++; });
                }
            }

            while (count != nowCount)
                yield return null;
            onFadeComplete?.Invoke();
        }
    }
}
