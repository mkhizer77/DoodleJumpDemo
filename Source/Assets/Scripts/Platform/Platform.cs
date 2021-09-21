using UnityEngine;
using DG.Tweening;

namespace MKK.DoodleJumpe.Platform
{
    public class Platform : PlatformBase
    {
        public override void ApplyPlatformEffect()
        {
            transform.DOPunchPosition(Vector3.down *.3f, .2f , 10, .5f);
            base.ApplyPlatformEffect();
        }
    }
}
