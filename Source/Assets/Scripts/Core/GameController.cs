using MKK.DoodleJumpe.Utility.Camera;
using System.Collections.Generic;
using MKK.DoodleJumpe.Player;
using MKK.DoodleJumpe.UI;
using UnityEngine;
using DG.Tweening;

namespace MKK.DoodleJumpe.Core
{
    public enum GameState
    {
        GameReady = 0,
        GamePlay = 1,
        GameOver = 2
    }
    public class GameController : MonoBehaviour
    {
        HashSet<IUpdateReciever> _updateRecievers = new HashSet<IUpdateReciever>();
        HashSet<IFixedUpdateReciever> _fixedUpdateRecievers = new HashSet<IFixedUpdateReciever>();

        public GameState GameState;

        [SerializeField] private PlayerController _playerController;
        [SerializeField] private LevelGenerator _levelGenerator;
        [SerializeField] private UIController _uIController;

        [SerializeField] private CameraFollow _cameraFollow;
        private int _score = 0;

        private GamePlayScreen _gamePlayScreen;
        private GameOverScreen _gameOverScreen;

        public void StartPlay()
        {
            GameState = GameState.GamePlay;
            _uIController.ShowScreen(ScreenId.GamePlay);

            if (_gamePlayScreen == null)
            {
                _gamePlayScreen = _uIController.CurrentScreen as GamePlayScreen;
            }
            _playerController.GetRigidBody().isKinematic = false;
        }

        public void CreateNewGame()
        {
            GameState = GameState.GameReady;
            _uIController.ShowScreen(ScreenId.MainMenu);
            _cameraFollow.Reset();
            _playerController.Init(this);
            _levelGenerator.Init(this,_playerController);
        }

        public void GameOver()
        {
            _cameraFollow.enabled = false;
            _playerController.GetRigidBody().isKinematic = true;
            Camera.main.DOShakePosition(.5f, 1, 10);
            GameState = GameState.GameOver;
            _uIController.ShowScreen(ScreenId.GameOver);

            if (_gameOverScreen == null)
            {
                _gameOverScreen = _uIController.CurrentScreen as GameOverScreen;
            }
            _gameOverScreen.UpdateScoreWithAnimation(_score);
        }

        public void ReStartGame()
        {
            DisposeGame();
            CreateNewGame();
        }


        public void AddScore() {
            _score++;
            _gamePlayScreen.UpdateScoreWithAnimation(_score);
        }

        private void DisposeGame()
        {
            _score = 0;
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
            CreateNewGame();
        }

        // Update is called once per frame
        private void Update()
        {
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