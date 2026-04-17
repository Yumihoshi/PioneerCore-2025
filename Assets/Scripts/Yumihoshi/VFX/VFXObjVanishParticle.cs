// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/09 17:04
// @version: 1.0
// @description:
// *****************************************************************************

using HoshiVerseFramework.Base.VFX;
using Yumihoshi.Singletons;
using AudioType = Yumihoshi.Singletons.AudioType;

namespace Yumihoshi.VFX
{
    public class VFXObjVanishParticle : VFXBaseParticleEntity
    {
        public override void Play()
        {
            AudioManager.Instance.PlayAudio(AudioType.Sfx, "Audios/SFX/GetObj");
            base.Play();
        }

        public override void Release()
        {
            AudioManager.Instance.StopAudio(AudioType.Sfx);
            base.Release();
        }
    }
}
