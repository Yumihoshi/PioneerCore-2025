// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/19 13:35
// @version: 1.0
// @description:
// *****************************************************************************

using System;
using UnityEngine;
using UnityEngine.UI;

namespace Yumihoshi.VFX
{
    public class VFXCutscene : MonoBehaviour
    {
        private static readonly int
            IsBlackID = Animator.StringToHash("IsBlack");

        private Animator _animator;
        private Image _img;

        private void Awake()
        {
            _img = GetComponent<Image>();
            _animator = GetComponent<Animator>();
            // 如果有多个过场物体，删除多余的
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Cutscene");
            if (gos.Length > 1)
                Destroy(transform.parent.gameObject);
            DontDestroyOnLoad(transform.parent);
        }

        private void Start()
        {
            FadeOut();
        }

        public event Action OnToBlackFinishedEvent;
        public event Action OnToClearFinishedEvent;

        private void OnToBlackFinished()
        {
            OnToBlackFinishedEvent?.Invoke();
        }

        private void OnToClearFinished()
        {
            OnToClearFinishedEvent?.Invoke();
        }

        /// <summary>
        /// 过场黑屏
        /// </summary>
        public void FadeIn()
        {
            _animator.SetBool(IsBlackID, true);
        }

        /// <summary>
        /// 过场亮屏
        /// </summary>
        public void FadeOut()
        {
            _animator.SetBool(IsBlackID, false);
        }

        /// <summary>
        /// 清除所有事件
        /// </summary>
        public void ClearEvents()
        {
            OnToClearFinishedEvent = null;
            OnToBlackFinishedEvent = null;
        }
    }
}
