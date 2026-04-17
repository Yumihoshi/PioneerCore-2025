// *****************************************************************************
// @author: Yumihoshi
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/30 15:07
// @version: 1.0
// @description:
// *****************************************************************************

using Yumihoshi.Task;

namespace Yumihoshi.Events
{
    /// <summary>
    /// 进入离开房间事件
    /// </summary>
    public class OnEnterExitRoomEvent
    {
        public RoomType RoomType { get; set; }
        public bool IsEnterOrExit { get; set; }
    }
}
