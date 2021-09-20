using UnityEngine;

namespace MKK.DoodleJumpe.Platform
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private float _bounceForce =10f;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.relativeVelocity.y <= 0)
            {
                Rigidbody2D playerBody = collision.gameObject.GetComponent<Rigidbody2D>();
                Vector2 velocity = playerBody.velocity;
                velocity.y = _bounceForce;
                playerBody.velocity = velocity;
            }
        }
    }
}
