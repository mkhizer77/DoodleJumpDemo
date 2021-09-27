using System.Collections;
using System.Collections.Generic;
using MKK.DoodleJumpe.Player;
using MKK.DoodleJumpe.Platform;
using UnityEngine;
using System;
using MKK.DoodleJumpe.Utility.Pool;

namespace MKK.DoodleJumpe.Core
{
    public interface IDespawner
    {
        public void DeSpawnPlatform(PlatformBase platformBase);
    }

    [Serializable]
    public struct LevelPlatform
    {
        public PlatformBase PlatformPrefab;
        [Range(0,1)] public float Occurance;
    }
    public class LevelGenerator : MonoBehaviour, IDisposable, IUpdateReciever, IDespawner
    {
        [SerializeField] private LevelPlatform[] _levelPlatform;
        [SerializeField] private float _spawnOffsetFromBottom = 2;

        private GameController _gameController;
        private IPlayer _player;
        private Vector2 _screenBounds;

        private Vector3 _levelStartPosition;
        private Vector3 _currentSpawnPositon;
        private float _playerY = 0;

        private HashSet<PlatformBase> _spawnedPlatfroms = new HashSet<PlatformBase>();

        private void Awake()
        {
            _screenBounds = Utils.GetScreenXYBoundsInWorldSpace();
            _levelStartPosition = new Vector3(0, -(_screenBounds.y - _spawnOffsetFromBottom), 0);
            _currentSpawnPositon = _levelStartPosition;
        }

        public void Init(GameController gameController, IPlayer player)
        {
            _gameController = gameController;
            _player = player;

            _gameController.SubscribeForUpdates(this);

            SpawnPlatform(_levelPlatform[0].PlatformPrefab,new Vector3(0,_player.GetY()-1f,0));
        }

        public void Dispose()
        {
            _currentSpawnPositon = _levelStartPosition;

            _gameController.UnsubscibeForUpdates(this);

            List<PlatformBase> platformsToDispose = new List<PlatformBase>();
            foreach(PlatformBase platformBase in _spawnedPlatfroms)
            {
                platformsToDispose.Add(platformBase);
            }
            foreach (PlatformBase platformBase in platformsToDispose)
            {
                platformBase.Dispose();
            }
            _spawnedPlatfroms.Clear();
        }

        public void DeSpawnPlatform(PlatformBase platformBase)
        {
            if (_spawnedPlatfroms.Contains(platformBase))
            {
                GameObjectPool.Despawn(platformBase.gameObject);
                _spawnedPlatfroms.Remove(platformBase);
            }
        }
        public void OnUpdate()
        {
            _playerY = _player.GetY();
            if (_playerY > _currentSpawnPositon.y || (_currentSpawnPositon.y - _playerY)< (_screenBounds.y * 10)) {
                SpawnNextLevelObject();
            }
        }

        private void SpawnPlatform(PlatformBase platformBase, Vector3 position)
        {
            var newObject = GameObjectPool.Spawn(platformBase, position, Quaternion.identity, transform);
            newObject.Init(this, _player);

            _spawnedPlatfroms.Add(newObject);
        }
        private void SpawnNextLevelObject()
        {
            float randomOccuranceValue = UnityEngine.Random.Range(0f, 1f);
            float randomOccuranceValue2 = UnityEngine.Random.Range(0f, 1f);
            float randomOccuranceValue3 = UnityEngine.Random.Range(0f, 1f);

            float _randromValue = randomOccuranceValue + randomOccuranceValue2 + randomOccuranceValue3;
            _randromValue /= 3;

            Vector3 nextObjectPosition = _currentSpawnPositon;
            float bumpYAfterSpawn = .60f + UnityEngine.Random.Range(.5f,1f);
            int nextPlatformId = 0;

            float xRange = Utils.GetScreenXYBoundsInWorldSpace().x - .63f;

            float randomX = UnityEngine.Random.Range(-xRange,xRange);
            while (Mathf.Abs(randomX - _currentSpawnPositon.x) > 2f)
            {
                randomX = UnityEngine.Random.Range(-xRange, xRange);
            }

            for (int i = _levelPlatform.Length - 1; i >= 0; i--)
            {
                if(_randromValue <= _levelPlatform[i].Occurance)
                {
                    nextPlatformId = i;
                    break;
                }
            }

            PlatformBase nextPlatform = _levelPlatform[nextPlatformId].PlatformPrefab;

            if (nextPlatform is MovingPlatform)
            {
                var _movingPlatform = nextPlatform as MovingPlatform;
                if(_movingPlatform.MovementAxis == MovementAxis.Vertical)
                {
                    nextObjectPosition.y += 1.5f;
                    bumpYAfterSpawn += 1f;
                }
                else
                {
                    float newXRange = xRange - .90f;
                    randomX = UnityEngine.Random.Range(-newXRange,newXRange);
                }
            }

            nextObjectPosition.x = randomX;

            SpawnPlatform(nextPlatform,nextObjectPosition);

           _currentSpawnPositon.y = nextObjectPosition.y + bumpYAfterSpawn;
            _currentSpawnPositon.x = nextObjectPosition.x;
        }
    }
}