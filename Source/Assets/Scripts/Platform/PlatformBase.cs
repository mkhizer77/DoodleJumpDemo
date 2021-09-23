using MKK.DoodleJumpe.Utility.Pool;
using MKK.DoodleJumpe.Player;
using MKK.DoodleJumpe.Core;
using DG.Tweening;
using UnityEngine;

namespace MKK.DoodleJumpe.Platform
{
    public abstract class PlatformBase : MonoBehaviour
    {
        [SerializeField] private float _bounceForce = 10f;

        private IPlayer _player;
        private IDespawner _despawner;
        public void Init(IDespawner despawner, IPlayer player)
        {
            _despawner = despawner;
            _player = player;

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
                _player.OnCollision(this);
            }
        }

        public virtual void ApplyPlatformEffect()
        {
            Vector2 velocity = _player.GetRigidBody().velocity;
            velocity.y = _bounceForce;
            _player.GetRigidBody().velocity = velocity;
        }

        public virtual void OnInit()
        {

        }
        public virtual void OnDispose()
        {
            _despawner.DeSpawnPlatform(this);
        }

        private void OnBecameInvisible()
        {
            if(_player.GetY() > transform.position.y)
            {
                Dispose();
            }
        }
    }
}