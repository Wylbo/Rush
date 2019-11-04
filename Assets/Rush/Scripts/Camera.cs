///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 21/10/2019 17:06
///-----------------------------------------------------------------

using Com.IsartDigital.Common;
using UnityEngine;

namespace Com.IsartDigital.Rush {
    public class Camera : MonoBehaviour {
        private static Camera instance;


        [SerializeField] private float speed;
        [SerializeField] private string horizontalAxis;
        [SerializeField] private string verticalAxis;
        [SerializeField] private string mouseBtn;

        [SerializeField] private float MouseSensitivity;
        [SerializeField] private float OribitDampening;

        private float vertAngle;
        private float horiAngle;

        private float distance;


        public static Camera Instance { get { return instance; } }

        private void Awake() {
            if (instance) {
                Destroy(gameObject);
                return;
            }
        }

        private void Start() {
            distance = Vector3.Distance(transform.position, Vector3.zero);
        }

        private void LateUpdate() {
            Move();
        }

        private void OnDestroy() {
            if (this == instance) instance = null;
        }

        private void Move() {
            if (Input.GetAxis(mouseBtn) != 0) {
                verticalAxis = "Mouse Y";
                horizontalAxis = "Mouse X";
                
            } else {
                verticalAxis = "Vertical";
                horizontalAxis = "Horizontal";
            }

            if (Input.GetAxis(verticalAxis) != 0) {
                vertAngle += Input.GetAxis(verticalAxis) * speed * Time.deltaTime;
                Debug.Log(vertAngle);
                vertAngle = Mathf.Clamp(vertAngle, -Mathf.PI / 2 + 0.1f, Mathf.PI / 2 - 0.1f);
            }

            if (Input.GetAxis(horizontalAxis) != 0) {
                horiAngle += Input.GetAxis(horizontalAxis) * speed * Time.deltaTime;

            }


            transform.position = MathTools.SphericalToCarthesian(distance, vertAngle, horiAngle);
            transform.LookAt(Vector3.zero);
        }
    }
}