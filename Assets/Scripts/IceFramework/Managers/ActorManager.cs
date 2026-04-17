using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IceFramework
{
    [Serializable]
    public class DotweenDate
    {
        [LabelText("移动时间")] public float time;
        [LabelText("移动曲线")] public Ease Ease;
    }

    public class ActorManager : MonoSingleton<ActorManager>
    {
        [LabelText("角色移动数据")] public DotweenDate MoveAnimationDate; //角色移动数据
        [LabelText("当前控制角色")] public Actor ControlActor;

        protected override void Init()
        {
            base.Init();
            ControlActor =
                GameObject.FindWithTag("Player")?.GetComponent<Actor>();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            ControlActor =
                GameObject.FindWithTag("Player")?.GetComponent<Actor>();
            if (!ControlActor)
                Debug.LogWarning("未找到玩家");
        }
    }
}
