// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/21 19:52
// @version: 1.0
// @description:
// *****************************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yumihoshi.Singletons;

namespace Yumihoshi.Cg
{
    public class CgEnterHouse : MonoBehaviour
    {
        [Header("进入别墅CG图片列表")] [SerializeField]
        private List<Sprite> spriteList;

        private Image _img;

        private void Awake()
        {
            _img = GetComponent<Image>();
        }

        private void Start()
        {
            Play();
        }

        /// <summary>
        /// 播放进入别墅CG
        /// </summary>
        public void Play(Action onFinished = null)
        {
            StartCoroutine(PlayCoroutine(onFinished));
        }

        private IEnumerator PlayCoroutine(Action onFinished)
        {
            foreach (Sprite sprite in spriteList)
            {
                _img.sprite = sprite;
                yield return new WaitForSeconds(0.5f);
            }

            onFinished?.Invoke();
            SceneLoader.Instance.LoadNextScene();
        }
    }
}
