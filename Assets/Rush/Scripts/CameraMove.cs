///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 21/10/2019 17:06
///-----------------------------------------------------------------

using Com.IsartDigital.Common;
using System;
using UnityEngine;

namespace Com.IsartDigital.Rush {
    public class CameraMove : MonoBehaviour {
        private static CameraMove instance;


        [SerializeField] private float speed;
        [SerializeField] private string horizontalAxis;
        [SerializeField] private string verticalAxis;
        [SerializeField] private string mouseBtn;
        [SerializeField] private string mouseHorizontalAxis;
        [SerializeField] private string mouseVerticalAxis;

        [SerializeField] private float mouseSensitivity;
        [SerializeField] private float OribitDampening;
        [SerializeField] private Transform cameraPivot;

        private (float hori, float vert) angles;

        private float distance;

        public Vector3 toPivot { get; private set; }

        public event Action<CameraMove> OnMove;


        public static CameraMove Instance { get { return instance; } }

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

        private void Update() {
            Move();
            toPivot = cameraPivot.position - transform.position;
        }

        private void OnDestroy() {
            if (this == instance) instance = null;
        }

        private (float hori, float vert) GetAxis() {
            (float hori, float vert) axis = (0, 0);

            if (Input.GetAxis(mouseBtn) != 0) {
                axis = (-Input.GetAxis(mouseHorizontalAxis) * mouseSensitivity, -Input.GetAxis(mouseVerticalAxis) * mouseSensitivity);
            } else {
                axis = (Input.GetAxis(horizontalAxis), Input.GetAxis(verticalAxis));
            }

            return axis;
        }

        private void Move() {
            (float hori, float vert) axis = GetAxis();

            angles.hori += axis.hori * speed * Time.deltaTime;
            angles.vert += axis.vert * speed * Time.deltaTime;

            angles.vert = Mathf.Clamp(angles.vert, -Mathf.PI / 2 + 0.1f, Mathf.PI / 2 - 0.1f);


            transform.position = MathTools.SphericalToCarthesian(distance, angles.vert, angles.hori) + cameraPivot.position;

            transform.LookAt(cameraPivot);

            OnMove?.Invoke(this);
        }
    }
}