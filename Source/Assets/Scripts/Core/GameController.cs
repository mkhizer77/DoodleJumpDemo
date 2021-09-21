using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MKK.DoodleJumpe.Player;

namespace MKK.DoodleJumpe.Core
{
    public class GameController : MonoBehaviour
    {
        //public static GameController Instance = null;

        HashSet<IUpdateReciever> _updateRecievers = new HashSet<IUpdateReciever>();
        HashSet<IFixedUpdateReciever> _fixedUpdateRecievers = new HashSet<IFixedUpdateReciever>();

        [SerializeField] private PlayerController _playerController;
        [SerializeField] private LevelGenerator _levelGenerator;

        //private void Awake()
        //{
        //    if (Instance == null)
        //    {
        //        Instance = this;
        //        DontDestroyOnLoad(this);
        //    }
        //    else
        //    {
        //        if(Instance != this)
        //        {
        //            Destroy(gameObject);
        //        }
        //    }
        //}

        public void StartGame()
        {
            _playerController.Init(this);
            _levelGenerator.Init(this);
        }

        public void GameOver()
        {
            Debug.Log("GameOver");
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