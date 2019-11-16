///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 21/10/2019 17:06
///-----------------------------------------------------------------

using Com.IsartDigital.Common;
using System;
using UnityEngine;

namespace Com.IsartDigital.Rush {
    public class CameraController : MonoBehaviour {
        private static CameraController instance;


        [SerializeField] private float speed;
        [SerializeField] private string horizontalAxis;
        [SerializeField] private string verticalAxis;
        [SerializeField] private string mouseBtn;
        [SerializeField] private string mouseHorizontalAxis;
        [SerializeField] private string mouseVerticalAxis;

        [SerializeField] private float mouseSensitivity;
        [SerializeField] private float OribitDampening;
        [SerializeField] public Transform cameraPivot;

        public (float hori, float vert) angles;

        public float distance;

        public Vector3 toPivot { get; private set; }

        public event Action<CameraController> OnMove;
        public static CameraController Instance { get { return instance; } }

        private void Awake() {
            if (instance) {
                Destroy(gameObject);
                return;
            }

            instance = this;
        }

        private void Start() {
            distance = Vector3.Distance(transform.position, cameraPivot.position);

        }

        private void OnDestroy() {
            if (this == instance) instance = null;

            OnMove = null;
        }



        private (float hori, float vert) GetAxis() {
            (float hori, float vert) axis = (0, 0);

#if UNITY_WEBGL || UNITY_EDITOR
            if (Input.GetAxis(mouseBtn) != 0) {
                axis = (-Input.GetAxis(mouseHorizontalAxis) * mouseSensitivity, -Input.GetAxis(mouseVerticalAxis) * mouseSensitivity);
            } else {
                axis = (Input.GetAxis(horizontalAxis), Input.GetAxis(verticalAxis));
            }
#endif

#if UNITY_ANDROID || UNITY_EDITOR
            if (Input.touchCount >= 2) {
                Touch touch = Input.GetTouch(1);

                if (touch.deltaPosition.magnitude > 1) {
                    axis = (touch.deltaPosition.x, touch.deltaPosition.y);
                }
            }
#endif

            OnMove?.Invoke(this);
            return axis;
        }


        public Vector3 Position() {
            (float hori, float vert) axis = GetAxis();

            angles.hori += axis.hori * speed * Time.deltaTime;
            angles.vert += axis.vert * speed * Time.deltaTime;

            angles.vert = Mathf.Clamp(angles.vert, -Mathf.PI / 2 + 0.1f, Mathf.PI / 2 - 0.1f);

            toPivot = cameraPivot.position - transform.position;

            return (MathTools.SphericalToCarthesian(distance, angles.vert, angles.hori) + cameraPivot.position);

        }

    }
}