// *****************************************************************************
// @author: Yumihoshi
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/05/03 14:04
// @version: 1.0
// @description:
// *****************************************************************************

using HoshiVerseFramework.Singletons;
using Sirenix.OdinInspector;
using UnityEngine;
using Yumihoshi.Events;
using Yumihoshi.Task;

namespace Yumihoshi.Entities
{
    public class EnterRoomPos : MonoBehaviour
    {
        [Header("进入房间点配置")] [SerializeField] [LabelText("房间类型")]
        private RoomType roomType;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;

            Debug.Log($"[房间] 进入房间{roomType.ToString()}");

            EventCenterManager.Instance.TriggerEvent(new OnEnterExitRoomEvent
            {
                IsEnterOrExit = true,
                RoomType = roomType
            });
        }
    }
}
