// *****************************************************************************
// @author: Yumihoshi
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/05/03 18:04
// @version: 1.0
// @description:
// *****************************************************************************

using HoshiVerseFramework.Singletons;
using IceFramework.InputManager;
using NodeCanvas.DialogueTrees;
using UnityEngine;
using Yumihoshi.Events;
using Yumihoshi.Task;

namespace Yumihoshi.Entities.Level1.DaTing
{
    public class ChuWuJianEnter : MonoBehaviour
    {
        private DialogueTreeController _dialogueTreeController;

        private void Awake()
        {
            _dialogueTreeController = GetComponent<DialogueTreeController>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            Debug.Log("[触发点] 玩家进入储物间触发点，开始对话");
            EventCenterManager.Instance.TriggerEvent(new OnEnterExitRoomEvent
            {
                IsEnterOrExit = true,
                RoomType = RoomType.ChuWuJianLevel1
            });
            _dialogueTreeController.StartDialogue();
        }

        public void SetPlayerMovable(bool status)
        {
            InputManager.Instance.AllowPlayerMove = status;
        }
    }
}
