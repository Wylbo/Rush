///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 21/10/2019 17:06
///-----------------------------------------------------------------

using Com.IsartDigital.Common;
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

        [SerializeField] private float MouseSensitivity;
        [SerializeField] private float OribitDampening;
        [SerializeField] private Transform cameraPivot;

        private (float, float) angles;

        private float distance;

        public Vector3 toPivot { get; private set; }


        public static CameraMove Instance { get { return instance; } }

        private void Awake() {
            if (instance) {
                Destroy(gameObject);
                return;
            }
        }

        private void Start() {
            distance = Vector3.Distance(transform.position, cameraPivot.position);

        }

        private void LateUpdate() {
            Move();
            toPivot = cameraPivot.position - transform.position;
        }

        private void OnDestroy() {
            if (this == instance) instance = null;
        }


        private (float, float) GetAxis() {
            if (Input.GetAxis(mouseBtn) != 0) {
                return (-Input.GetAxis(mouseHorizontalAxis), -Input.GetAxis(mouseVerticalAxis));
            }
            return (Input.GetAxis(horizontalAxis), Input.GetAxis(verticalAxis));

        }

        private void Move() {

            angles.Item1 += GetAxis().Item1 * speed * Time.deltaTime;
            angles.Item2 += GetAxis().Item2 * speed * Time.deltaTime;

            angles.Item2 = Mathf.Clamp(angles.Item2, -Mathf.PI / 2 + 0.1f, Mathf.PI / 2 - 0.1f);


            transform.position = MathTools.SphericalToCarthesian(distance, angles.Item2, angles.Item1) + cameraPivot.position;

            transform.LookAt(cameraPivot);
        }
    }
}