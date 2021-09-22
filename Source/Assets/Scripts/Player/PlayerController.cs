using MKK.DoodleJumpe.Core;
using UnityEngine;

namespace MKK.DoodleJumpe.Player
{
    public interface IPlayer
    {
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

        void Awake()
        {
            _playerStartY = GetY();
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
            _moveX = 0;
            _rigidBody.velocity = Vector2.zero;
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