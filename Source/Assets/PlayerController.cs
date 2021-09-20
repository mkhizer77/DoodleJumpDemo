using UnityEngine;

namespace MKK.DoodleJumpe.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidBody;

        [SerializeField] private float _movementSpeed = 5;

        private float _input = 0;
        private Vector2 _playerVelocity;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {
            _input = Input.GetAxis("Horizontal") * _movementSpeed;
        }

        private void FixedUpdate()
        {
            _playerVelocity = _rigidBody.velocity;
            _playerVelocity.x = _input;
            _rigidBody.velocity = _playerVelocity;
        }
    }
}