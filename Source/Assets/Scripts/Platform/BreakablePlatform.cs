using UnityEngine;
using DG.Tweening;
using System.Collections;

namespace MKK.DoodleJumpe.Platform
{
    public class BreakablePlatform : PlatformBase
    {
        [SerializeField] private EdgeCollider2D _edgeCollider2D;
        [SerializeField] private Transform[] _breakableBrickTransforms;

        private Vector3[] _brickStartLocalPositions;

        public override void OnInit()
        {
            _brickStartLocalPositions = new Vector3[_breakableBrickTransforms.Length];

            for (int i = 0; i < _breakableBrickTransforms.Length; i++)
            {
                _brickStartLocalPositions[i] = _breakableBrickTransforms[i].localPosition;
            }
        }
        public override void OnDispose()
        {
            _edgeCollider2D.enabled = true;

            for (int i = 0; i < _breakableBrickTransforms.Length; i++)
            {
                _breakableBrickTransforms[i].localPosition = _brickStartLocalPositions[i];
                _breakableBrickTransforms[i].localEulerAngles = Vector3.zero;
            }
        }
        public override void ApplyPlatformEffect()
        {
            transform.DOPunchPosition(Vector3.down * .3f, .2f, 10, .5f);
            base.ApplyPlatformEffect();
            StartCoroutine(BreakPlatformRoutine());
        }

        IEnumerator BreakPlatformRoutine()
        {
            yield return new WaitForSeconds(.1f);
            BreakPlatform();
        }
        private void BreakPlatform()
        {
            _edgeCollider2D.enabled = false;
            foreach(Transform brickTransfrom in _breakableBrickTransforms)
            {
                var rigidBody2D = brickTransfrom.gameObject.AddComponent<Rigidbody2D>();
                rigidBody2D.AddRelativeForce(new Vector2(Random.Range(-.5f,.5f), Random.Range(-.2f, .2f)),ForceMode2D.Impulse);
                rigidBody2D.AddTorque(Random.Range(-.5f,.5f),ForceMode2D.Impulse);    
            }
        }
    }
}