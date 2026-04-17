// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/09 16:04
// @version: 1.0
// @description:
// *****************************************************************************

using System.Collections.Generic;
using HoshiVerseFramework.Base;
using UnityEngine;

namespace Yumihoshi.Singletons
{
    public class AudioManager : Singleton<AudioManager>
    {
        private readonly Dictionary<AudioType, AudioSource> _audioSourceDict =
            new();

        private AudioSource[] _audioSources;

        protected override void Awake()
        {
            base.Awake();
            _audioSources = GetComponents<AudioSource>();
            _audioSourceDict.TryAdd(AudioType.Bgm, _audioSources[0]);
            _audioSourceDict.TryAdd(AudioType.Environment, _audioSources[1]);
            _audioSourceDict.TryAdd(AudioType.Sfx, _audioSources[2]);
        }

        /// <summary>
        /// 播放音频
        /// </summary>
        /// <param name="type"></param>
        /// <param name="path"></param>
        /// <param name="loop"></param>
        public void PlayAudio(AudioType type, string path, bool loop = false)
        {
            AudioClip clip = CheckAudio(path, out bool success);
            if (!success)
            {
                Debug.LogError($"[Audio] 音频播放失败：未找到音频文件{path}");
                return;
            }

            PlayAudio(type, clip, loop);
        }

        /// <summary>
        /// 播放音频
        /// </summary>
        /// <param name="type"></param>
        /// <param name="clip"></param>
        /// <param name="loop"></param>
        public void PlayAudio(AudioType type, AudioClip clip, bool loop = false)
        {
            if (!_audioSourceDict.TryGetValue(type,
                    out AudioSource audioSource))
            {
                Debug.LogError($"[Audio] 音频播放失败：未找到音频组件{type}");
                return;
            }

            audioSource.clip = clip;
            audioSource.loop = loop;
            // TODO: 渐近渐出
            audioSource.Stop();
            audioSource.Play();
        }

        /// <summary>
        /// 检查音频是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <param name="success"></param>
        /// <returns></returns>
        private AudioClip CheckAudio(string path, out bool success)
        {
            var clip = Resources.Load<AudioClip>(path);
            if (!clip)
            {
                success = false;
                return null;
            }

            success = true;
            return clip;
        }

        /// <summary>
        /// 停止音频
        /// </summary>
        /// <param name="type"></param>
        public void StopAudio(AudioType type)
        {
            if (!_audioSourceDict.TryGetValue(type,
                    out AudioSource audioSource))
            {
                Debug.LogError($"[Audio] 音频播放失败：未找到音频组件{type}");
                return;
            }

            audioSource.Stop();
        }

        /// <summary>
        /// 暂停音频
        /// </summary>
        /// <param name="type"></param>
        public void PauseAudio(AudioType type)
        {
            if (!_audioSourceDict.TryGetValue(type,
                    out AudioSource audioSource))
            {
                Debug.LogError($"[Audio] 音频播放失败：未找到音频组件{type}");
                return;
            }

            audioSource.Pause();
        }

        /// <summary>
        /// 恢复音频
        /// </summary>
        /// <param name="type"></param>
        public void ResumeAudio(AudioType type)
        {
            if (!_audioSourceDict.TryGetValue(type,
                    out AudioSource audioSource))
            {
                Debug.LogError($"[Audio] 音频播放失败：未找到音频组件{type}");
                return;
            }

            audioSource.UnPause();
        }

        /// <summary>
        /// 停止所有音频
        /// </summary>
        public void StopAll()
        {
            foreach (AudioSource source in _audioSources)
                source.Stop();
        }
    }

    public enum AudioType
    {
        /// <summary>
        /// 背景音乐
        /// </summary>
        Bgm = 1,

        /// <summary>
        /// 环境音乐
        /// </summary>
        Environment,

        /// <summary>
        /// 音效
        /// </summary>
        Sfx
    }
}
