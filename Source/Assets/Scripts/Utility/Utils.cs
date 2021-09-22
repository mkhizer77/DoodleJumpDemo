using UnityEngine;

public static class Utils 
{
    public static Vector2 GetScreenXYBoundsInWorldSpace()
    {
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        return screenBounds;
    }
}
