///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 21/10/2019 17:06
///-----------------------------------------------------------------

using Com.IsartDigital.Common;
using UnityEngine;

namespace Com.IsartDigital.Rush {
    public class Camera : MonoBehaviour {
        private static Camera instance;

        protected Transform _XFormCamera;
        protected Transform _XFormParent;

        protected Vector3 _LocalRotation;

        [SerializeField] private float speed;
        [SerializeField] private string horizontalAxis;
        [SerializeField] private string verticalAxis;
        [SerializeField] private string mouseX;
        [SerializeField] private string mouseY;
        [SerializeField] private string mouseBtn;
        
        [SerializeField] private float MouseSensitivity;
        [SerializeField] private float OribitDampening;


        public static Camera Instance { get { return instance; } }

        private void Awake() {
            if (instance) {
                Destroy(gameObject);
                return;
            }
        }

        private void Start() {
            _XFormCamera = transform;
            _XFormParent = transform.parent;
        }

        private void LateUpdate() {
            if (Input.GetAxis(mouseBtn) != 0) {
                if (Input.GetAxis(mouseX) != 0 || Input.GetAxis(mouseY) != 0) {
                    _LocalRotation.x += Input.GetAxis(mouseX) * MouseSensitivity;
                    _LocalRotation.y -= Input.GetAxis(mouseY) * MouseSensitivity;
                }
            } else {
                if (Input.GetAxis(horizontalAxis) != 0 || Input.GetAxis(verticalAxis) != 0) {
                    _LocalRotation.x -= Input.GetAxis(horizontalAxis) * speed;
                    _LocalRotation.y -= Input.GetAxis(verticalAxis) * speed;
                }
            }
            _LocalRotation.y = Mathf.Clamp(_LocalRotation.y, -90, 90);

            Quaternion QT = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
            _XFormParent.rotation = Quaternion.Lerp(_XFormParent.rotation, QT, Time.deltaTime * OribitDampening);
        }

        private void OnDestroy() {
            if (this == instance) instance = null;
        }
    }
}