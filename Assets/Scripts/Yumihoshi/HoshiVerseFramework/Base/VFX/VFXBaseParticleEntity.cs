// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/08 20:04
// @version: 1.0
// @description:
// *****************************************************************************

using HoshiVerseFramework.Interfaces;
using UnityEngine;

namespace HoshiVerseFramework.Base.VFX
{
    /// <summary>
    /// 带有对象池的粒子特效实体基类
    /// </summary>
    public class VFXBaseParticleEntity : VFXBaseEntityWithPool, IVFX
    {
        protected ParticleSystem particle;

        protected virtual void Awake()
        {
            particle = GetComponent<ParticleSystem>();
            Stop();
        }

        /// <summary>
        /// 播放粒子特效
        /// </summary>
        public virtual void Play()
        {
            particle.Play();
        }

        /// <summary>
        /// 暂停粒子特效
        /// </summary>
        public virtual void Pause()
        {
            particle.Pause();
        }

        /// <summary>
        /// 停止粒子特效
        /// </summary>
        public virtual void Stop()
        {
            particle.Stop();
        }

        /// <summary>
        /// 恢复粒子特效
        /// </summary>
        public virtual void Resume()
        {
            particle.Play();
        }

        /// <summary>
        /// 释放粒子特效
        /// </summary>
        public virtual void Release()
        {
            pool.Release(gameObject);
        }
    }
}
