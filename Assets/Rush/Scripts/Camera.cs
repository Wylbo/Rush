///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 21/10/2019 17:06
///-----------------------------------------------------------------

using Com.IsartDigital.Common;
using UnityEngine;

namespace Com.IsartDigital.Rush {
    public class Camera : MonoBehaviour {
        private static Camera instance;

        [SerializeField] private Vector3 pivot;
        [SerializeField] private float speed;
        [SerializeField] private string horizontalAxis;
        [SerializeField] private string verticalAxis;
        [SerializeField] private float angleCap;

        private float distanceFromPivot;

        public static Camera Instance { get { return instance; } }

        private void Awake() {
            if (instance) {
                Destroy(gameObject);
                return;
            }

            instance = this;
            transform.LookAt(pivot);

        }

        private void Start() {
            distanceFromPivot = Vector3.Distance(pivot, transform.position);
        }

        private void Update() {
            MoveKeyboard();
        }

        private void MoveKeyboard() {
            float vertAngle = Input.GetAxis(verticalAxis);
            float horiAngle = Input.GetAxis(horizontalAxis);

            if (Vector3.Angle(Vector3.up, transform.forward) < angleCap && vertAngle < 0) {
                vertAngle = 0;
            }
            if (Vector3.Angle(Vector3.down, transform.forward) < angleCap && vertAngle > 0) {
                vertAngle = 0;
            }

            transform.LookAt(pivot);
            transform.Translate(new Vector3(horiAngle, vertAngle) * (Time.deltaTime * speed));

        }

        private void OnDestroy() {
            if (this == instance) instance = null;
        }
    }
}