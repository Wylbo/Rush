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
        [SerializeField] private string mouseX;
        [SerializeField] private string mouseY;
        [SerializeField] private string mouseBtn;
        [SerializeField] private float angleCap;
        [SerializeField] private bool InvertYAxis;
        [SerializeField] private bool InvertXAxis;

        private float distanceFromPivot;
        private Vector3 mouseStartPos;

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

            float vertAngle = 0;
            float horiAngle = 0;

            if (Input.GetButton(mouseBtn)) {
                if (Input.GetButtonDown(mouseBtn)) {
                    mouseStartPos = Input.mousePosition;
                }

                Vector3 direction = Input.mousePosition - mouseStartPos;

                vertAngle = Mathf.Clamp(direction.y / 100, -1, 1);
                horiAngle = Mathf.Clamp(direction.x, -1, 1);

                vertAngle *= InvertYAxis ? -1 : 1;
                horiAngle *= InvertXAxis ? -1 : 1;

            } else {
                vertAngle = Input.GetAxis(verticalAxis);
                horiAngle = Input.GetAxis(horizontalAxis);
            }

            Move(vertAngle, horiAngle);
        }

        private void Move(float vertAngle, float horiAngle) {

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