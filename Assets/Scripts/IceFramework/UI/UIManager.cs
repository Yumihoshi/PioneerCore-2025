using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IceFramework.UI
{
    public class UIManager : MonoSingleton<UIManager>
    {
        [LabelText("待触发UI状态")] [Tooltip("按下UI确认键后会进入该状态")]
        public UIState TriggerState;

        [SerializeField] [LabelText("当前UI状态")] public UIState nowState;

        private void Update()
        {
            nowState.Update?.Invoke();
        }

        public void OnQuit(InputAction.CallbackContext inputValue)
        {
            if (inputValue.started) nowState.Quit?.Invoke();
        }

        public void OnMove(InputAction.CallbackContext inputValue)
        {
            if (inputValue.started)
                nowState.Move?.Invoke(inputValue.ReadValue<Vector2>());
        }

        public void OnConfirm(InputAction.CallbackContext inputValue)
        {
            if (inputValue.started) nowState.Confirm?.Invoke();
        }

        public void Player_OnPause(InputAction.CallbackContext inputValue)
        {
            if (inputValue.started) nowState.Quit?.Invoke();
        }

        public void Player_OnConfirm(InputAction.CallbackContext inputValue)
        {
            if (inputValue.started)
                if (TriggerState != null)
                    SetState(TriggerState);
        }

        /// <summary>
        /// 修改UI状态
        /// </summary>
        public void SetState(UIState uiState)
        {
            if (uiState is Idle)
                InputManager.InputManager.Instance.SwitchMap("Player");
            else
                InputManager.InputManager.Instance.SwitchMap("UI");
            nowState = uiState;
        }

        /// <summary>
        /// 用于暂存触发的UI状态
        /// </summary>
        public void SetTriggerState(UIState triggerState)
        {
            TriggerState = triggerState;
        }
    }
}
