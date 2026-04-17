using System.Collections;
using System.Collections.Generic;
using IceFramework;
using IceFramework.EventManager;
using IceFramework.ResourceManager;
using UnityEngine;

namespace Game.Sound
{
    public class SoundManager : MonoSingleton<SoundManager>
    {
        public float fadeTime;

        private readonly List<AudioSource> effectSoundSources = new();
        private AudioSource bgmSource;

        private void Start()
        {
            //DontDestroyOnLoad(gameObject);
            EventManager.Instance.AddEventListener<string>("播放背景音乐", PlayBGM);
            EventManager.Instance.AddEventListener<string>("切换背景音乐", SwitchBGM);
            EventManager.Instance.AddEventListener<string>("播放音效",
                PlaySoundEffect);
            EventManager.Instance.AddEventListener<string>("随机播放音效",
                PlaySoundEffectRandom);
            EventManager.Instance.AddEventListener("停止播放音效", CancleEffect);
            //effectSoundSources.Add(GetComponents<AudioSource>()[1]);
        }

        protected override void Init()
        {
            base.Init();
            bgmSource = GetComponents<AudioSource>()[0];
            effectSoundSources.Add(GetComponents<AudioSource>()[1]);
        }

        public void GetBGMSourceValue(out float value)
        {
            value = bgmSource.volume;
        }

        public void SetBGMSourceValue(float value)
        {
            bgmSource.volume = value;
        }

        public void GetEffectSoundValue(out float value)
        {
            value = effectSoundSources[0].volume;
        }

        public void SetEffectSoundValue(float value)
        {
            foreach (var audioSource in effectSoundSources)
                audioSource.volume = value;
        }

        private void PlayBGM(string bgmName)
        {
            var path = "Sounds/" + bgmName;
            var res = ResManager.LoadResource<AudioClip>(path);
            bgmSource.clip = res;
            bgmSource.loop = true;
            bgmSource.Play();
        }

        public void PlaySoundEffectRandom(string flieName)
        {
            var path = "Sounds/" + flieName;
            AudioClip[] audioSources =
                ResManager.LoadResources<AudioClip>(path);
            int randomNum = Random.Range(0, audioSources.Length - 1);
            foreach (var audioSource in effectSoundSources)
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = audioSources[randomNum];
                    audioSource.Play();
                    return;
                }

            AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
            newAudioSource.clip = audioSources[randomNum];
            newAudioSource.Play();
            newAudioSource.volume = effectSoundSources[0].volume;
            effectSoundSources.Add(newAudioSource);
        }

        public void PlaySoundEffect(string effectName)
        {
            var path = "Sounds/" + effectName;
            var res = ResManager.LoadResource<AudioClip>(path);
            foreach (var audioSource in effectSoundSources)
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = res;
                    audioSource.Play();
                    return;
                }

            AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
            newAudioSource.clip = res;
            newAudioSource.Play();
            newAudioSource.volume = effectSoundSources[0].volume;
            effectSoundSources.Add(newAudioSource);
        }

        private void CancleEffect()
        {
            foreach (var audioSource in effectSoundSources) audioSource.Stop();
        }

        private void SwitchBGM(string targetBGM)
        {
            var path = "Sounds/" + targetBGM;
            var res = ResManager.LoadResource<AudioClip>(path);
            StartCoroutine(FadeCoroutine(fadeTime, res));
        }

        private IEnumerator FadeCoroutine(float fadeTime, AudioClip res)
        {
            float startVolume = bgmSource.volume;
            while (bgmSource.volume > 0f)
            {
                bgmSource.volume -= startVolume * Time.deltaTime / fadeTime;
                yield return null;
            }

            bgmSource.clip = res;
            bgmSource.Play();
            while (bgmSource.volume < startVolume)
            {
                bgmSource.volume += Time.deltaTime / fadeTime;

                yield return null;
            }

            bgmSource.volume = startVolume;
        }
    }
}
