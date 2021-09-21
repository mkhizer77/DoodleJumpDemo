using UnityEngine;
using DG.Tweening;
using System.Collections;

namespace MKK.DoodleJumpe.Platform
{
    public class SpringPlatform : PlatformBase
    {
        [SerializeField] private SpriteRenderer _springRender;
        [SerializeField] Sprite _normalSpringSprite;
        [SerializeField] Sprite _pressedSpringSprite;

        public override void ApplyPlatformEffect()
        {
            StartCoroutine(SpringEffectRoutine());
            base.ApplyPlatformEffect();
        }

        IEnumerator SpringEffectRoutine()
        {
            _springRender.sprite = _pressedSpringSprite;
            transform.DOPunchPosition(Vector3.up * .1f, .1f, 10, .5f);
            yield return new WaitForSeconds(.1f);
            _springRender.sprite = _normalSpringSprite;
        }
    }
}