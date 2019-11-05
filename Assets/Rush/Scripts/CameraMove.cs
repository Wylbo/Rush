///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 21/10/2019 17:06
///-----------------------------------------------------------------

using Com.IsartDigital.Common;
using UnityEngine;

namespace Com.IsartDigital.Rush {
    public class CameraMove : MonoBehaviour {
        private static Camera instance;


        [SerializeField] private float speed;
        [SerializeField] private string horizontalAxis;
        [SerializeField] private string verticalAxis;
        [SerializeField] private string mouseBtn;
        [SerializeField] private string mouseHorizontalAxis;
        [SerializeField] private string mouseVerticalAxis;

        [SerializeField] private float MouseSensitivity;
        [SerializeField] private float OribitDampening;

        private (float, float) angles;

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


        private (float, float) GetAxis() {
            if (Input.GetAxis(mouseBtn) != 0) {
                return (-Input.GetAxis(mouseHorizontalAxis), -Input.GetAxis(mouseVerticalAxis));
            }
            return ( Input.GetAxis(horizontalAxis), Input.GetAxis(verticalAxis));

        }

        private void Move() {

            angles.Item1 += GetAxis().Item1 * speed * Time.deltaTime;
            angles.Item2 += GetAxis().Item2 * speed * Time.deltaTime;

            angles.Item2 = Mathf.Clamp(angles.Item2, -Mathf.PI / 2 + 0.1f, Mathf.PI / 2 - 0.1f);


            transform.position = MathTools.SphericalToCarthesian(distance, angles.Item2, angles.Item1);
            transform.LookAt(Vector3.zero);
        }
    }
}