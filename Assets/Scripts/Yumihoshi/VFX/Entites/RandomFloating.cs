// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/14 12:04
// @version: 1.0
// @description:
// *****************************************************************************

using System.Collections;
using DG.Tweening;
using UnityEngine;
using Yumihoshi.VFX.Entites.ShaderEffect;
using Random = UnityEngine.Random;

namespace Yumihoshi.VFX.Entites
{
    public class RandomFloating : MonoBehaviour
    {
        private static readonly int EnableID = Shader.PropertyToID("_Enable");

        [Header("随机浮动参数")] [SerializeField] private float radius = 1f;

        [SerializeField] private float interval = 2f;

        [SerializeField] private float movingDuration = 1f;
        private bool _enable;

        private Material _mat;
        private Vector3 _startPos = Vector3.zero;
        private VFXFloating _vfxFloating;

        /// <summary>
        /// 随机浮动半径
        /// </summary>
        public float Radius => radius;

        /// <summary>
        /// 随机浮动间隔
        /// </summary>
        public float Interval => interval;

        /// <summary>
        /// 移动时间
        /// </summary>
        public float MovingDuration => movingDuration;

        private void Awake()
        {
            _vfxFloating = new VFXFloating();
        }


        private void Start()
        {
            StartCoroutine(TT());
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(
                _startPos == Vector3.zero ? transform.position : _startPos,
                radius);
        }

        private IEnumerator TT()
        {
            yield return new WaitForSeconds(1);
            EnableRandomFloat();
        }

        /// <summary>
        /// 启用随机浮动
        /// </summary>
        public void EnableRandomFloat()
        {
            if (_enable)
            {
                Debug.LogWarning("[VFX] 随机浮动已经启用，不可重复启用");
                return;
            }

            _mat = GetComponent<Renderer>().material;
            // 禁用其他Shader
            if (_mat.shader.name == _vfxFloating.FullShaderName)
                _mat.SetInt(EnableID, 0);
            _startPos = transform.position;
            StartCoroutine(FloatCoroutine());
            _enable = true;
        }

        /// <summary>
        /// 停止随机浮动
        /// </summary>
        public void StopRandomFloat()
        {
            StopAllCoroutines();
            _enable = false;
        }

        private IEnumerator FloatCoroutine()
        {
            while (true)
            {
                transform.DOKill();
                // 生成随机点
                Vector2 randomDir =
                    Random.insideUnitCircle.normalized; // 生成一个随机方向
                float randomDistance = Random.Range(0f, radius); // 生成一个随机距离
                Vector2 randomPoint =
                    (Vector2)_startPos + randomDir * randomDistance; // 计算随机点
                // 移动到随机点
                transform.DOMove(randomPoint, movingDuration);
                yield return new WaitForSeconds(interval);
            }
        }
    }
}
