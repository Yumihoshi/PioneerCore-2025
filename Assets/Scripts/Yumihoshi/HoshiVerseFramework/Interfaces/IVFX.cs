// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/09 15:04
// @version: 1.0
// @description:
// *****************************************************************************

namespace HoshiVerseFramework.Interfaces
{
    public interface IVFX
    {
        /// <summary>
        /// 开始播放特效
        /// </summary>
        public void Play();

        /// <summary>
        /// 暂停特效
        /// </summary>
        public void Pause();

        /// <summary>
        /// 停止特效，并将播放进度重置为0
        /// </summary>
        public void Stop();

        /// <summary>
        /// 重启特效，立即播放
        /// </summary>
        public void Restart()
        {
            Stop();
            Play();
        }

        /// <summary>
        /// 恢复特效播放（继续播放特效）
        /// </summary>
        public void Resume();

        /// <summary>
        /// 释放特效资源
        /// </summary>
        public void Release();
    }
}
