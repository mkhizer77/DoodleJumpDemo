using UnityEngine;
using DG.Tweening;
using System.Collections;

namespace MKK.DoodleJumpe.Platform
{

    public enum MovementAxis
    {
        Horizontal,
        Vertical
    }
    public class MovingPlatform : PlatformBase 
    {
        public MovementAxis MovementAxis;
        [SerializeField] private float _moveUnits = 2;
        [SerializeField] private float _moveSpeed = 2;

        private Tween _movementTween;
        private Vector2 _startPosition;
        private Vector2 _targetMaxPosition;
        private Vector2 _targetMinPosition;

        private bool _reverseMovement;

        public override void OnInit()
        {
            _reverseMovement = Random.Range(0, 1) == 0 ? true : false;

            _startPosition = transform.position;

            _targetMaxPosition = _startPosition;
            _targetMinPosition = _startPosition;

            if(MovementAxis == MovementAxis.Horizontal) {
                _targetMaxPosition.x = (_startPosition.x + _moveUnits / 2);
                _targetMinPosition.x = (_startPosition.x - _moveUnits / 2);
            }
            else
            {
                _targetMaxPosition.y = (_startPosition.y + _moveUnits / 2);
                _targetMinPosition.y = (_startPosition.y - _moveUnits / 2);
            }
            transform.position = _reverseMovement ? _targetMaxPosition : _targetMinPosition;

            SetupTween();
        }

        public override void OnDispose()
        {
            base.OnDispose();
        }

        public override void ApplyPlatformEffect()
        {
            base.ApplyPlatformEffect();
        }

        void SetupTween()
        {
            _movementTween = transform.DOMove(_reverseMovement? _targetMinPosition : _targetMaxPosition, _moveUnits / _moveSpeed);
            _movementTween.SetEase(Ease.Linear);
            _movementTween.SetLoops(-1, LoopType.Yoyo);
        }
    }
}
