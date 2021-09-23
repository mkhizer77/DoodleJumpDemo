using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKK.DoodleJumpe.UI
{
    public class UIController : MonoBehaviour
    {
        Dictionary<ScreenId, UIScreenBase> _screens = new Dictionary<ScreenId, UIScreenBase>();

        public UIScreenBase CurrentScreen;

        void Awake()
        {
            var screens = GetComponentsInChildren<UIScreenBase>(true);
            foreach(UIScreenBase uIScreen in screens)
            {
                uIScreen.Hide();
                _screens.Add(uIScreen.ScreenId,uIScreen);
            }

            CurrentScreen = screens[0];
            CurrentScreen.Show();
        }

        public void ShowScreen(ScreenId screenIdToShow)
        {
            if (CurrentScreen.ScreenId == screenIdToShow)
                return;

            CurrentScreen.Hide();
            CurrentScreen = _screens[screenIdToShow];
            CurrentScreen.Show();
        }
    }
}