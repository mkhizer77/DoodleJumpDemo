using MKK.DoodleJumpe.Player;
using DG.Tweening;
using UnityEngine;

namespace MKK.DoodleJumpe.Platform
{
    public abstract class PlatformBase : MonoBehaviour
    {
        [SerializeField] private float _bounceForce = 10f;

        private IPlayerRigidBody _playerRigidBody;

        public void Init(Vector3 spawnPosition, IPlayerRigidBody playerRigidBody)
        {
            transform.position = spawnPosition;
            _playerRigidBody = playerRigidBody;

            OnInit();
        }

        public void Dispose()
        {
            transform.DOKill();

            OnDispose();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.relativeVelocity.y <= 0)
            {
                ApplyPlatformEffect();
            }
        }

        public virtual void ApplyPlatformEffect()
        {
            Vector2 velocity = _playerRigidBody.GetBody().velocity;
            velocity.y = _bounceForce;
            _playerRigidBody.GetBody().velocity = velocity;
        }

        public virtual void OnInit()
        {

        }
        public virtual void OnDispose()
        {
            
        }
    }
}