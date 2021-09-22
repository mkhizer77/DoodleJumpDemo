using UnityEngine;

namespace MKK.DoodleJumpe.Utility.Camera
{
    public class ScreenBounds : MonoBehaviour
    {
        [SerializeField] BoxCollider2D _leftBound;
        [SerializeField] BoxCollider2D _rightBound;

        void Awake()
        {
            AddCollider();
        }

        void AddCollider()
        {
            if (UnityEngine.Camera.main == null) { Debug.LogError("Camera.main not found, failed to create edge colliders"); return; }

            var screenBounds = Utils.GetScreenXYBoundsInWorldSpace();

            _leftBound.size = new Vector2(_leftBound.size.x, screenBounds.y * 2);
            _leftBound.transform.position = new Vector3(-screenBounds.x, _leftBound.transform.position.y, _leftBound.transform.position.z);

            _rightBound.size = new Vector2(_rightBound.size.x, screenBounds.y * 2);
            _rightBound.transform.position = new Vector3(screenBounds.x, _rightBound.transform.position.y, _rightBound.transform.position.z);
        }
    }
}