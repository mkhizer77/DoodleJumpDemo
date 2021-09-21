using System.Collections;
using System.Collections.Generic;
using MKK.DoodleJumpe.Player;
using MKK.DoodleJumpe.Platform;
using UnityEngine;
using System;

namespace MKK.DoodleJumpe.Core
{
    public class LevelGenerator : MonoBehaviour, IDisposable, IUpdateReciever
    {
        [SerializeField] private PlatformBase[] _platformPrefabs;
        [SerializeField] PlayerController _playerController;
        private Vector2 _screenBounds;

        GameController _gameController;

        private void Awake()
        {
            _screenBounds = Utils.GetScreenXYBoundsInWorldSpace();
        }

        public void Init(GameController gameController)
        {
            _gameController = gameController;
            _gameController.SubscribeForUpdates(this);
            foreach (PlatformBase platform in _platformPrefabs)
            {
                platform.Init(platform.transform.position, _playerController);
            }
        }

        public void Dispose()
        {
            _gameController.UnsubscibeForUpdates(this);
        }

        public void OnUpdate()
        {

        }
    }
}