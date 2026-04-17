using System.Collections.Generic;
using IceFramework.SaveDataManager;
using NodeCanvas.Framework;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

public class InteractableObject : MonoBehaviour
{
    [LabelText("交互事件")] [Tooltip("玩家与其交互时触发该事件")]
    public UnityEvent InterEvent;

    public Blackboard Blackboard;
    public string BlackboardJson;
    private int Id;

    private void Awake()
    {
        TryGetComponent(out Blackboard);
    }

    private void OnEnable()
    {
        if (Blackboard != null) //如果物体有黑板组件
        {
            Id = gameObject.GetInstanceID(); //获取唯一实例id
            if (SaveDataManagerObject.Instance.PlayerData
                .ObjectBlackboardDictionary.TryGetValue(Id, out BlackboardJson))
                //如果获取到了黑板数据，则反序列化，否则保持默认状态
                Blackboard.Deserialize(BlackboardJson, new List<Object>());
        }
    }

    private void OnDisable()
    {
        if (Blackboard != null)
            SaveDataManagerObject.Instance.PlayerData
                    .ObjectBlackboardDictionary[Id] =
                Blackboard.Serialize(new List<Object>());
    }
}
