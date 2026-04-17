// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/22 15:40
// @version: 1.0
// @description:
// *****************************************************************************

using System.Collections;
using HoshiVerseFramework.Base;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Yumihoshi.VFX;

namespace Yumihoshi.Singletons
{
    public class SceneLoader : Singleton<SceneLoader>
    {
        [LabelText("场景加载开始事件")] public UnityEvent onStartSceneLoadEvent;

        /// <summary>
        /// 场景加载完成，黑屏消失后事件
        /// </summary>
        [LabelText("场景加载完成事件")] public UnityEvent onSceneLoadedEvent;

        private VFXCutscene _vfxCutscene;

        private void Start()
        {
            // 获取过场物体
            _vfxCutscene = GameObject.FindWithTag("Cutscene")
                .GetComponent<VFXCutscene>();
            if (_vfxCutscene) return;
            Debug.LogError("[VFX] 过场物体未找到");
        }

        public void LoadNextScene()
        {
            LoadNextScene(LoadSceneMode.Single);
        }

        /// <summary>
        /// 加载下一个场景
        /// </summary>
        public void LoadNextScene(LoadSceneMode mode)
        {
            onStartSceneLoadEvent?.Invoke();
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            LoadScene(buildIndex + 1, mode);
        }

        /// <summary>
        /// 结合了过场黑屏亮屏的场景加载
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="mode"></param>
        public void LoadScene(string sceneName,
            LoadSceneMode mode = LoadSceneMode.Single)
        {
            onStartSceneLoadEvent?.Invoke();
            _vfxCutscene.OnToBlackFinishedEvent += () =>
                StartCoroutine(LoadSceneAsync(sceneName, mode));
            _vfxCutscene.OnToClearFinishedEvent +=
                () => onSceneLoadedEvent?.Invoke();
            _vfxCutscene.OnToClearFinishedEvent += _vfxCutscene.ClearEvents;
            _vfxCutscene.FadeIn();
        }

        /// <summary>
        /// 结合了过场黑屏亮屏的场景加载
        /// </summary>
        /// <param name="buildIndex"></param>
        /// <param name="mode"></param>
        public void LoadScene(int buildIndex,
            LoadSceneMode mode = LoadSceneMode.Single)
        {
            onStartSceneLoadEvent?.Invoke();
            _vfxCutscene.OnToBlackFinishedEvent += () =>
                StartCoroutine(LoadSceneAsync(buildIndex, mode));
            _vfxCutscene.OnToClearFinishedEvent +=
                () => onSceneLoadedEvent?.Invoke();
            _vfxCutscene.OnToClearFinishedEvent += _vfxCutscene.ClearEvents;
            _vfxCutscene.FadeIn();
        }

        private IEnumerator LoadSceneAsync(string sceneName,
            LoadSceneMode mode = LoadSceneMode.Single)
        {
            // 检验场景是否存在
            AsyncOperation asyncLoad =
                SceneManager.LoadSceneAsync(
                    sceneName, mode);
            if (asyncLoad == null)
            {
                Debug.LogError($"[SceneManager] 场景加载失败，{sceneName}不存在");
                yield break;
            }

            // 异步加载
            asyncLoad.allowSceneActivation = false;
            while (!asyncLoad.isDone)
            {
                Debug.Log(
                    $"[场景管理器] 加载{sceneName}进度为：{asyncLoad.progress * 100}%");
                if (asyncLoad.progress >= 0.9f)
                {
                    asyncLoad.allowSceneActivation = true;
                    _vfxCutscene.FadeOut();
                }

                yield return null;
            }
        }

        private IEnumerator LoadSceneAsync(int buildIndex,
            LoadSceneMode mode = LoadSceneMode.Single)
        {
            // 检验场景是否存在
            AsyncOperation asyncLoad =
                SceneManager.LoadSceneAsync(
                    buildIndex);
            if (asyncLoad == null)
            {
                Debug.LogError($"[SceneManager] 场景加载失败，索引{buildIndex}不存在");
                yield break;
            }

            // 异步加载
            asyncLoad.allowSceneActivation = false;
            while (!asyncLoad.isDone)
            {
                Debug.Log(
                    $"[场景管理器] 加载{buildIndex}进度为：{asyncLoad.progress * 100}%");
                if (asyncLoad.progress >= 0.9f)
                {
                    asyncLoad.allowSceneActivation = true;
                    _vfxCutscene.FadeOut();
                }

                yield return null;
            }
        }
    }
}
