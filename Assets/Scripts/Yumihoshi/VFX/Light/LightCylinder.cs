// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/12 17:04
// @version: 1.0
// @description:
// *****************************************************************************

using HoshiVerseFramework.Base.VFX;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering.Universal;

namespace Yumihoshi.VFX.Light
{
    public class LightCylinder : VFXBaseParticleEntity
    {
        private static readonly int CompleteShowHash =
            Animator.StringToHash("CompleteShow");

        private static readonly int ShowHash = Animator.StringToHash("Show");

        private static readonly int CompleteHide =
            Animator.StringToHash("CompleteHide");

        private Animator _animator;
        private Light2D _light;
        private PlayableDirector _playableDirector;

        protected override void Awake()
        {
            particle = transform.Find("Particle")
                .GetComponent<ParticleSystem>();
            particle.Stop();
            _light = GetComponent<Light2D>();
            _animator = GetComponent<Animator>();
            _playableDirector = GetComponent<PlayableDirector>();
        }

        public override void Play()
        {
            _playableDirector.Play();
        }

        public override void Pause()
        {
            _playableDirector.Pause();
        }

        public override void Stop()
        {
            _playableDirector.Stop();
        }

        /// <summary>
        /// <para>显示光柱</para>
        /// <para>注：若要播放特效，请使用IVFX接口的Play方法</para>
        /// </summary>
        /// <param name="status">显示状态</param>
        /// <param name="immediate">是否立即完全显示</param>
        public void DisplayLightCylinder(bool status = true,
            bool immediate = false)
        {
            if (status)
            {
                if (immediate)
                    _animator.SetTrigger(CompleteShowHash);
                else
                    _animator.SetBool(ShowHash, true);
            }
            else
            {
                if (immediate)
                    _animator.SetTrigger(CompleteHide);
                else
                    _animator.SetBool(ShowHash, false);
            }
        }

        /// <summary>
        /// 设置光柱颜色
        /// </summary>
        /// <param name="color"></param>
        public void SetLightColor(Color color)
        {
            _light.color = color;
        }
    }
}
