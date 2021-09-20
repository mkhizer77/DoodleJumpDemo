using UnityEngine;
using DG.Tweening;

namespace MKK.DoodleJumpe.Platform
{
    public class Platform : PlatformBase
    {
        public override void ApplyPlatformEffect()
        {
            transform.DOShakePosition(.3f,Vector3.down*.1f,5);
            base.ApplyPlatformEffect();
        }
    }
}
