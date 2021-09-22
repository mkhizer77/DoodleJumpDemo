using UnityEngine;
using System.Collections;

namespace MKK.DoodleJumpe.UI
{
    public class MainMenuScreen : UIScreenBase
    {
        [SerializeField] private Animation _animation;
        [SerializeField] private GameObject _tapToPlayText;

        private void Awake()
        {
            ScreenId = ScreenId.MainMenu;
            _tapToPlayText.SetActive(false);
        }

        private void OnEnable()
        {
            _animation.Play();
            StartCoroutine(ShowPlayTextRoutine());
        }

        IEnumerator ShowPlayTextRoutine()
        {
            yield return new WaitForSeconds(2);
            _tapToPlayText.SetActive(true);
        }

        private void OnDisable()
        {
            _tapToPlayText.SetActive(false);
        }
    }
}