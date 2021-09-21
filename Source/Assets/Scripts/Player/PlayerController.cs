using MKK.DoodleJumpe.Core;
using UnityEngine;

namespace MKK.DoodleJumpe.Player
{
    public interface IPlayerRigidBody
    {
        public Rigidbody2D GetBody();
    }
    public class PlayerController : MonoBehaviour, IPlayerRigidBody, IUpdateReciever, IFixedUpdateReciever
    {
        [SerializeField] private Rigidbody2D _rigidBody;
        [SerializeField] private float _movementSpeed = 5;

        private float _moveX = 0;
        private Vector2 _playerVelocity;

        GameController _gameController;

        public void Init(GameController gameController)
        {
            _gameController = gameController;
            _gameController.SubscribeForUpdates(this);
            _gameController.SubscribeForFixedUpdates(this);
        }

        public void Dispose()
        {
            _gameController.UnsubscibeForUpdates(this);
            _gameController.UnsubscibeForFixedUpdates(this);
        }

        public Rigidbody2D GetBody()
        {
            return _rigidBody;
        }

        public void OnUpdate()
        {
            _moveX = Input.GetAxis("Horizontal") * _movementSpeed;
        }

        public void OnFixedUpdate()
        {
            _playerVelocity = _rigidBody.velocity;
            _playerVelocity.x = _moveX;
            _rigidBody.velocity = _playerVelocity;
        }

        private void OnBecameInvisible()
        {
            Kill();
        }

        private void Kill()
        {
            _gameController.GameOver();
        }
    }
}