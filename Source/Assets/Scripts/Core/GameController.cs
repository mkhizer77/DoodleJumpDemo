using MKK.DoodleJumpe.Utility.Camera;
using System.Collections.Generic;
using MKK.DoodleJumpe.Player;
using MKK.DoodleJumpe.UI;
using UnityEngine;

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

        public void StartPlay()
        {
            GameState = GameState.GamePlay;
            _uIController.ShowScreen(ScreenId.GamePlay);
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
            GameState = GameState.GameOver;
            _uIController.ShowScreen(ScreenId.GameOver);
        }

        public void ReStartGame()
        {
            DisposeGame();
            CreateNewGame();
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