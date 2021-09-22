using UnityEngine;
using System.Collections.Generic;
using MKK.DoodleJumpe.Player;
using MKK.DoodleJumpe.Utility.Camera;

namespace MKK.DoodleJumpe.Core
{
    public enum GameState
    {
        MainMenu = 0,
        GamePlay = 1,
        GameOver = 2
    }
    public class GameController : MonoBehaviour
    {
        HashSet<IUpdateReciever> _updateRecievers = new HashSet<IUpdateReciever>();
        HashSet<IFixedUpdateReciever> _fixedUpdateRecievers = new HashSet<IFixedUpdateReciever>();

        [SerializeField] private GameState _gameState;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private LevelGenerator _levelGenerator;
        [SerializeField] private CameraFollow _cameraFollow;

       
        public void StartGame()
        {
            _gameState = GameState.GamePlay;
            _cameraFollow.Reset();
            _playerController.Init(this);
            _levelGenerator.Init(this,_playerController);
        }

        public void GameOver()
        {
            _gameState = GameState.GameOver;
        }

        public void ReStartGame()
        {
            DisposeGame();
            StartGame();
        }

        private void DisposeGame()
        {
            _playerController.Dispose();
            _levelGenerator.Dispose();
        }

        public void SubscribeForUpdates(IUpdateReciever updateReciever)
        {
            if (!_updateRecievers.Contains(updateReciever))
            {
                _updateRecievers.Add(updateReciever);
            }
        }

        public void UnsubscibeForUpdates(IUpdateReciever updateReciever)
        {
            if (_updateRecievers.Contains(updateReciever))
            {
                _updateRecievers.Remove(updateReciever);
            }
        }

        public void SubscribeForFixedUpdates(IFixedUpdateReciever fixedUpdateReciever)
        {
            if (!_fixedUpdateRecievers.Contains(fixedUpdateReciever))
            {
                _fixedUpdateRecievers.Add(fixedUpdateReciever);
            }
        }
        public void UnsubscibeForFixedUpdates(IFixedUpdateReciever fixedUpdateReciever)
        {
            if (_fixedUpdateRecievers.Contains(fixedUpdateReciever))
            {
                _fixedUpdateRecievers.Remove(fixedUpdateReciever);
            }
        }

        // Use this for initialization
        void Start()
        {
            StartGame();
        }

        // Update is called once per frame
        private void Update()
        {
            if (_gameState != GameState.GamePlay)
                return;

            foreach(IUpdateReciever updateReciever in _updateRecievers)
            {
                updateReciever.OnUpdate();
            }
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            foreach (IFixedUpdateReciever fixedUpdateReciever in _fixedUpdateRecievers)
            {
                fixedUpdateReciever.OnFixedUpdate();
            }
        }
    }
}