// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/09 13:04
// @version: 1.0
// @description:
// *****************************************************************************

using DG.Tweening;
using UnityEngine;

namespace Yumihoshi.UI
{
    public class ObjShake : MonoBehaviour
    {
        [Header("抖动参数")] [SerializeField] public DialogueShakeType shakeType =
            DialogueShakeType.Random;

        /// <summary>
        /// 对话框抖动持续时间
        /// </summary>
        [SerializeField] public float duration = 0.5f;

        /// <summary>
        /// 对话框随机抖动强度
        /// </summary>
        [Header("随机抖动参数")] [SerializeField] public float intensity = 20f;

        /// <summary>
        /// 对话框随机抖动频率
        /// </summary>
        [SerializeField] public int vibrato = 15;

        /// <summary>
        /// 对话框随机抖动随机度
        /// </summary>
        [SerializeField] public float randomness;

        /// <summary>
        /// 对话框非随机抖动循环次数
        /// </summary>
        [Header("非随机抖动参数")] [SerializeField] [Range(1, 50)]
        public int loopCount = 2;

        /// <summary>
        /// 对话框非随机抖动偏移距离
        /// </summary>
        [SerializeField] public float offset = 20f;

        /// <summary>
        /// 对话框非随机抖动动画类型
        /// </summary>
        [SerializeField] public Ease ease = Ease.OutBack;

        private Sequence _sequence;

        private void Awake()
        {
            ResetSequence();
        }

        /// <summary>
        /// 对话框抖动，重复调用会覆盖之前的抖动
        /// </summary>
        public void Shake()
        {
            ResetSequence();
            Vector3 originPos = transform.position;
            Vector3 newPos = originPos;
            Vector3 newPos2 = originPos;
            switch (shakeType)
            {
                case DialogueShakeType.HorizontalLeftToRight:
                    newPos.x += offset;
                    newPos2.x -= offset;
                    HandleMove(originPos, newPos, newPos2);

                    break;
                case DialogueShakeType.HorizontalRightToLeft:
                    newPos.x += offset;
                    newPos2.x -= offset;
                    HandleMove(originPos, newPos2, newPos);

                    break;
                case DialogueShakeType.VerticalBottomToUp:
                    newPos.y += offset;
                    newPos2.y -= offset;
                    HandleMove(originPos, newPos, newPos2);

                    break;
                case DialogueShakeType.VerticalUpToBottom:
                    newPos.y += offset;
                    newPos2.y -= offset;
                    HandleMove(originPos, newPos2, newPos);

                    break;
                case DialogueShakeType.PositiveDiagonal:
                    newPos.x += offset;
                    newPos.y += offset;
                    newPos2.x -= offset;
                    newPos2.y -= offset;
                    HandleMove(originPos, newPos, newPos2);

                    break;
                case DialogueShakeType.NegativeDiagonal:
                    newPos.x += offset;
                    newPos.y -= offset;
                    newPos2.x -= offset;
                    newPos2.y += offset;
                    HandleMove(originPos, newPos2, newPos);
                    break;
                case DialogueShakeType.Random:
                    _sequence.Append(transform.DOShakePosition(duration,
                        intensity, vibrato, randomness, false, false));
                    break;
                default:
                    Debug.LogError(
                        $"[VFX] 对话框抖动时错误，抖动类型{shakeType.ToString()}无法处理");
                    return;
            }
        }

        /// <summary>
        /// 重置动画序列
        /// </summary>
        /// <param name="complete">是否立即完成上个动画</param>
        private void ResetSequence(bool complete = true)
        {
            if (!complete)
                _sequence?.Kill();
            else
                _sequence?.Complete();
            _sequence = DOTween.Sequence();
        }

        /// <summary>
        /// 处理移动
        /// </summary>
        /// <param name="originPos"></param>
        /// <param name="newPos"></param>
        /// <param name="newPos2"></param>
        private void HandleMove(Vector3 originPos, Vector3 newPos,
            Vector3 newPos2)
        {
            for (var i = 0; i < loopCount; i++)
            {
                _sequence.Append(transform
                    .DOMove(newPos2, duration / 3 / loopCount)
                    .SetEase(ease));
                _sequence.Append(transform
                    .DOMove(newPos, duration / 3 / loopCount)
                    .SetEase(ease));
                _sequence.Append(transform
                    .DOMove(originPos, duration / 3 / loopCount)
                    .SetEase(ease));
            }
        }
    }

    public enum DialogueShakeType
    {
        /// <summary>
        /// 横向左右抖动（先右后左）
        /// </summary>
        HorizontalRightToLeft = 1,

        /// <summary>
        /// 横向左右抖动（先左后右）
        /// </summary>
        HorizontalLeftToRight,

        /// <summary>
        /// 纵向上下抖动（先上后下）
        /// </summary>
        VerticalUpToBottom,

        /// <summary>
        /// 纵向上下抖动（先下后上）
        /// </summary>
        VerticalBottomToUp,

        /// <summary>
        /// 正对角线（右上左下）抖动
        /// </summary>
        PositiveDiagonal,

        /// <summary>
        /// 负对角线（左上右下）抖动
        /// </summary>
        NegativeDiagonal,

        /// <summary>
        /// 随机抖动
        /// </summary>
        Random
    }
}
