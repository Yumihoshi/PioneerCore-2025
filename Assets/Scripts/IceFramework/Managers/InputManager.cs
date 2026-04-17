using IceFramework.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IceFramework.InputManager
{
    public class InputManager : MonoSingleton<InputManager>
    {
        [SerializeField] [ShowInInspector] private PlayerInput PlayerInput;

        [ShowInInspector] public Vector2Int moveValue;

        [ShowInInspector] public bool AllowPlayerMove { get; set; } = true;

        private void Update()
        {
            if (AllowPlayerMove)
                ActorManager.Instance.ControlActor?.Move(moveValue);
        }

        public void SwitchMap(string mapName)
        {
            if (!CheckMap(mapName)) PlayerInput.SwitchCurrentActionMap(mapName);
        }

        public bool CheckMap(string mapName)
        {
            if (PlayerInput.currentActionMap == null) return false;
            return mapName == PlayerInput.currentActionMap.name;
        }


        public void OnMove(InputValue inputValue)
        {
            Vector2 value = inputValue.Get<Vector2>();
            if (value.x != 0)
                moveValue = new Vector2Int((int)inputValue.Get<Vector2>().x, 0);
            else if (value.y != 0)
                moveValue = new Vector2Int(0, (int)inputValue.Get<Vector2>().y);
            else
                moveValue = Vector2Int.zero;
        }

        public void OnInteract(InputValue inputValue)
        {
            if (inputValue.isPressed)
                ActorManager.Instance.ControlActor.Interact();
        }

        public void OnUseItem1(InputValue inputValue)
        {
            if (inputValue.isPressed)
                ActorManager.Instance.ControlActor.Interact(
                    Idle.Instance.ShortCutItem[0]); //哈哈，我是单例高手
        }

        public void OnUseItem2(InputValue inputValue)
        {
            if (inputValue.isPressed)
                ActorManager.Instance.ControlActor.Interact(
                    Idle.Instance.ShortCutItem[1]);
        }

        public void OnUseItem3(InputValue inputValue)
        {
            if (inputValue.isPressed)
                ActorManager.Instance.ControlActor.Interact(
                    Idle.Instance.ShortCutItem[2]);
        }

        public void OnBag(InputValue inputValue)
        {
            if (inputValue.isPressed) Bag.Instance.Enter.Invoke();
        }

        public void OnUIBag(InputValue inputValue)
        {
            if (inputValue.isPressed)
            {
                print(111111);
                if (UIManager.Instance.nowState == Bag.Instance)
                    Bag.Instance.Quit.Invoke();
                else
                    UIManager.Instance.nowState.Enter.Invoke();
            }
        }

        public void OnUIMove(InputValue inputValue)
        {
            UIManager.Instance.nowState._Move(inputValue.Get<Vector2>());
        }

        public void OnUIConfirm(InputValue inputValue)
        {
            if (inputValue.isPressed) UIManager.Instance.nowState._Confirm();
        }

        public void OnUIQuit(InputValue inputValue)
        {
            if (inputValue.isPressed) UIManager.Instance.nowState.Quit.Invoke();
        }

        /// <summary>
        /// F4切换全屏
        /// </summary>
        /// <param name="inputValue"></param>
        public void OnSwitchFullscreen(InputValue inputValue)
        {
            if (inputValue.isPressed) Screen.fullScreen = !Screen.fullScreen;
        }
    }
}
