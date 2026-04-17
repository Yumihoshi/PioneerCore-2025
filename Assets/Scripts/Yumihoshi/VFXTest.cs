// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/09 13:04
// @version: 1.0
// @description:
// *****************************************************************************

using HoshiVerseFramework.Singletons;
using UnityEngine;
using Yumihoshi.Events;
using Yumihoshi.Task;

namespace Yumihoshi
{
    public class VFXTest : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            EventCenterManager.Instance.TriggerEvent(new OnEnterExitRoomEvent
            {
                IsEnterOrExit = true,
                RoomType = RoomType.XuanGuanLevel1
            });
        }
    }
}
