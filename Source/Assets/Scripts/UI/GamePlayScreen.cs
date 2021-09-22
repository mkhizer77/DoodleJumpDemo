using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;

namespace MKK.DoodleJumpe.UI
{
    public class GamePlayScreen : UIScreenBase
    {
       [SerializeField] private Text _scoreText;

        private Tweener _tweener;

        private void Awake()
        {
            ScreenId = ScreenId.GamePlay;
        }

        public void UpdateScoreWithAnimation(int score)
        {
            _scoreText.text = score.ToString();

            if (!_tweener.IsActive())
                _tweener = _scoreText.transform.DOPunchScale(1.5f * Vector3.one, .5f);
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