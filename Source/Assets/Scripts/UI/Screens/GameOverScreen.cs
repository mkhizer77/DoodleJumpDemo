using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MKK.DoodleJumpe.UI
{
    public class GameOverScreen : UIScreenBase
    {
        [SerializeField] private Text _scoreText;

        private Tweener _tweener;

        private void Awake()
        {
            ScreenId = ScreenId.GameOver;
        }

        public void UpdateScoreWithAnimation(int score)
        {
            _scoreText.text = score.ToString();

            if (!_tweener.IsActive())
                _tweener = _scoreText.transform.DOPunchScale(1.5f * Vector3.up, 1f,5);
        }

        private void OnEnable()
        {
            _scoreText.text = "0";
        }

        private void OnDisable()
        {
            _tweener.Kill();
            _scoreText.text = "0";
        }
    }
}