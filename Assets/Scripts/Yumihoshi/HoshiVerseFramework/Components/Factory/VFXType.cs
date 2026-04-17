// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/09 15:04
// @version: 1.0
// @description:
// *****************************************************************************

namespace HoshiVerseFramework.Components.Factory
{
    /// <summary>
    /// 特效类型，根据需要修改
    /// </summary>
    public enum VFXType
    {
        /// <summary>
        /// 物体消失粒子-边框五角星
        /// </summary>
        ObjVanishBorderStar = 1,

        /// <summary>
        /// 物体消失粒子-纯色星星
        /// </summary>
        ObjVanishStar,

        /// <summary>
        /// 物体消失粒子-小星星
        /// </summary>
        ObjVanishStarSmall,

        /// <summary>
        /// 物体消失金色星星
        /// </summary>
        ObjVanishStar2Small,

        /// <summary>
        /// 物体消失粒-边框小星星
        /// </summary>
        ObjVanishBorderStarSmall,

        /// <summary>
        /// 物体消失粒子-小辉星
        /// </summary>
        ObjVanishLightningStarSmall,

        /// <summary>
        /// 物体合成辉光
        /// </summary>
        BlendFlare,

        /// <summary>
        /// 物体合成失败-灰色烟雾
        /// </summary>
        BlendFail
    }
}
