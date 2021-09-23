using UnityEngine;
using System.Collections;

namespace MKK.DoodleJumpe.UI
{
    public enum ScreenId
    {
        MainMenu =0,
        GamePlay =1,
        GameOver =2
    }

    public abstract class UIScreenBase : MonoBehaviour
    {
        public ScreenId ScreenId;

        public void Show()
        {
            gameObject.SetActive(true);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}