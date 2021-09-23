using UnityEngine;

namespace MKK.DoodleJumpe.Utility.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _targetTransform;
        [SerializeField] private float _smoothDamp  = .5f;

        private Vector3 _cameraStartPosition;
        private Vector3 _cameraTargetPos;

        public void Reset()
        {
            transform.position = _cameraStartPosition;
            _cameraTargetPos = _cameraStartPosition;
            this.enabled = true;
        }

        private void Start()
        {
            _cameraTargetPos = transform.position;
            _cameraStartPosition = _cameraTargetPos;
        }

        void LateUpdate()
        {
            if(_targetTransform.position.y > transform.position.y)
            {
                _cameraTargetPos = new Vector3(transform.position.x,_targetTransform.position.y,transform.position.z);
            }
            transform.position = Vector3.Lerp(transform.position, _cameraTargetPos, _smoothDamp * Time.deltaTime);
        }
    }
}
