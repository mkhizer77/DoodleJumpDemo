using System.Collections.Generic;
using MKK.DoodleJumpe.Core;
using UnityEngine;

namespace MKK.DoodleJumpe.Player
{
    public interface IPlayer
    {
        public void OnCollision(Platform.PlatformBase platform);
        public Rigidbody2D GetRigidBody();
        public float GetY();
    }
    public class PlayerController : MonoBehaviour, IPlayer, IUpdateReciever, IFixedUpdateReciever
    {
        [SerializeField] private Rigidbody2D _rigidBody;
        [SerializeField] private float _movementSpeed = 5;

        private float _moveX = 0;
        private Vector2 _playerVelocity;

        private GameController _gameController;
        private float _playerStartY;
        private float _screenBottomY;

        private Transform _cameraTransform;
        HashSet<Platform.PlatformBase> _platformsAlreadyCollidedWith = new HashSet<Platform.PlatformBase>();

        void Awake()
        {
            _playerStartY = GetY();
            var size =  GetComponent<SpriteRenderer>().bounds.max.y - _playerStartY;
            var screenY = Utils.GetScreenXYBoundsInWorldSpace().y;

            _screenBottomY = -(size + screenY);
            _rigidBody.isKinematic = true;

            _cameraTransform = Camera.main.transform;
        }

        public void Init(GameController gameController)
        {
            transform.position = new Vector3(0,_playerStartY,0);
            _gameController = gameController;
            _gameController.SubscribeForUpdates(this);
            _gameController.SubscribeForFixedUpdates(this);
        }

        public void Dispose()
        {
            _platformsAlreadyCollidedWith.Clear();
            _moveX = 0;
            _rigidBody.velocity = Vector2.zero;
            _rigidBody.isKinematic = true;
            _gameController.UnsubscibeForUpdates(this);
            _gameController.UnsubscibeForFixedUpdates(this);
        }

        public Rigidbody2D GetRigidBody()
        {
            return _rigidBody;
        }
        public float GetY()
        {
            return transform.position.y;
        }
        public void OnUpdate()
        {
            if (_gameController.GameState == GameState.GamePlay)
            {
                _moveX = Input.GetAxis("Horizontal") * _movementSpeed;

                if (GetY() < (_cameraTransform.position.y +_screenBottomY))
                {
                    Kill();
                }
            }
        }

        public void OnFixedUpdate()
        {
            _playerVelocity = _rigidBody.velocity;
            _playerVelocity.x = _moveX;
            _rigidBody.velocity = _playerVelocity;
        }

        private void Kill()
        {
            _gameController.GameOver();
        }

        public void OnCollision(Platform.PlatformBase platform)
        {
            Handheld.Vibrate();
            if (!_platformsAlreadyCollidedWith.Contains(platform))
            {
                _platformsAlreadyCollidedWith.Add(platform);
                _gameController.AddScore();
            }
        }
    }
}