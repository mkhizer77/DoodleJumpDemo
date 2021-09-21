using UnityEngine;

namespace MKK.DoodleJumpe.Player
{
    public interface IPlayerRigidBody
    {
        public Rigidbody2D GetBody();
    }
    public class PlayerController : MonoBehaviour, IPlayerRigidBody
    {
        [SerializeField] private Rigidbody2D _rigidBody;

        [SerializeField] private float _movementSpeed = 5;

        private float _moveX = 0;
        private Vector2 _playerVelocity;

        // Start is called before the first frame update
        void Start()
        {

        }
        
        // Update is called once per frame
        private void Update()
        {
            _moveX = Input.GetAxis("Horizontal") * _movementSpeed;
        }

        private void FixedUpdate()
        {
            _playerVelocity = _rigidBody.velocity;
            _playerVelocity.x = _moveX;
            _rigidBody.velocity = _playerVelocity;
        }

        void OnBecameInvisible()
        {
            Debug.Log("** GameOver");
            Debug.Break();
        }

        public Rigidbody2D GetBody()
        {
            return _rigidBody;
        }
    }
}