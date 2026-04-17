using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

//对象池管理器
//分为调用端和存储端
//只处理对象的保存、添加、创建及启用

namespace IceFramework.ObjectsPoolManager
{
    #region ObjectsPool

    public class ObjectsPoolManager : SerializedMonoBehaviour
    {
        [ShowInInspector] [ReadOnly]
        public static List<GameObject> Pool = new();

        private void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
                Pool.Add(transform.GetChild(i).gameObject);
        }

        [Button("UpdatePool")]
        [ButtonGroup]
        private void UpdatePool()
        {
            for (int i = 0; i < transform.childCount; i++)
                Pool.Add(transform.GetChild(i).gameObject);
        }

        [Button("Clear")]
        [ButtonGroup]
        private void ClearPool()
        {
            Pool.Clear();
        }
    }

    public class ObjectsPool : SingletonBace<ObjectsPool>
    {
        private static GameObject ObjectsPoolManagerObject;
        private static readonly List<GameObject> Pool = ObjectsPoolManager.Pool;

        public ObjectsPool()
        {
            if (GameObject.Find("ObjectsPoolManager") != null)
            {
                ObjectsPoolManagerObject =
                    GameObject.Find("ObjectsPoolManager");
            }
            else
            {
                ObjectsPoolManagerObject = new GameObject("ObjectsPoolManager");
                ObjectsPoolManagerObject.AddComponent<ObjectsPoolManager>();
                ObjectsPoolManagerObject.SetActive(true);
                ObjectsPoolManagerObject.isStatic = true;
            }
        }

        public void Create(string objectName, int number)
        {
            for (int i = 0; i < number; i++)
            {
                string path = "Prefabs/" + objectName;
                GameObject newGameObject =
                    ObjectsPoolManager.Instantiate(
                        Resources.Load<GameObject>(path));
                newGameObject.name = objectName;
                Add(newGameObject);
            }
        }

        public void Add(GameObject gameObject)
        {
            Pool.Add(gameObject);
            gameObject.transform.parent = ObjectsPoolManagerObject.transform;
        }

        /// <summary>
        /// 获取物体
        /// </summary>
        /// <param ListenerName="name">名称</param>
        /// <param ListenerName="objectState">物体是否活跃？</param>
        /// <returns>使用时需要明确物体状态</returns>
        public GameObject Get(string name, bool objectState)
        {
            GameObject target = Pool.Find(i =>
                i.name == name && i.activeSelf == objectState);
            if (target != null) return target;
            Debug.LogWarning("未查找到对象");
            return null;
        }

        public GameObject Get(string name)
        {
            GameObject target = Pool.Find(i => i.name == name);
            if (target != null) return target;
            Debug.LogWarning("未查找到对象");
            return null;
        }

        public bool TryGet(string name, bool objectState,
            out GameObject gameObject)
        {
            gameObject = Pool.Find(i =>
                i.name == name && i.activeSelf == objectState);
            return gameObject != null;
        }

        public bool TryGet(string name, out GameObject gameObject)
        {
            gameObject = Pool.Find(i => i.name == name);
            return gameObject != null;
        }

        public List<GameObject> GetAll(string name, bool objectState)
        {
            return Pool.FindAll(i =>
                i.name == name && i.activeSelf == objectState);
        }

        public void Remove(GameObject gameObject)
        {
            Pool.Remove(gameObject);
            gameObject.transform.parent = null;
        }

        public void Remove(string name, bool objectState)
        {
            GameObject targgetGameObject = Get(name, objectState);
            Pool.Remove(targgetGameObject);
            targgetGameObject.transform.parent = null;
        }

        /// <summary>
        /// 设置物体是否激活
        /// </summary>
        /// <param name="gameObject">目标物体</param>
        /// <param name="targetState">要设置状态</param>
        /// <returns>是否存在于对象池中</returns>
        public bool SetActive(GameObject gameObject, bool targetState)
        {
            bool i = true;
            if (!Pool.Contains(gameObject))
            {
                Add(gameObject);
                i = false;
            }

            if (gameObject != null)
            {
                gameObject.SetActive(targetState);
                return i;
            }

            Debug.LogWarning("指定对象为空");
            return false;
        }

        public GameObject SetActive(string name, bool objectState,
            bool targetState)
        {
            if (TryGet(name, objectState, out GameObject gameObject))
            {
                gameObject.SetActive(targetState);
                return gameObject;
            }

            Debug.LogWarning("指定对象为空");
            return null;
        }

        public GameObject SetActive(string name, bool targetState)
        {
            if (TryGet(name, out GameObject gameObject))
            {
                gameObject.SetActive(targetState);
                return gameObject;
            }

            Debug.LogWarning("指定对象为空");
            return null;
        }

        public void SetActiveAll(List<GameObject> gameObjects, bool targetState)
        {
            foreach (GameObject gameObject in gameObjects)
                SetActive(gameObject, targetState);
        }

        public bool TrySetActive(string name, bool objectState,
            bool targetState, out GameObject gameObject)
        {
            bool i = TryGet(name, objectState, out GameObject _gameObject);
            _gameObject.SetActive(targetState);
            gameObject = _gameObject;
            return i;
        }

        public bool TrySetActive(string name, bool targetState,
            out GameObject gameObject)
        {
            bool i = TryGet(name, out GameObject _gameObject);
            _gameObject.SetActive(targetState);
            gameObject = _gameObject;
            return i;
        }
    }

    #endregion
}
