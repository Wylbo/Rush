///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 21/10/2019 17:06
///-----------------------------------------------------------------

using Com.IsartDigital.Common;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

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
        [SerializeField] private LayerMask groundMask;

        public (float hori, float vert) angles;

        public float distance;

        public Vector3 toPivot { get; private set; }

        public event Action<CameraController> OnMove;

        public delegate Ray RaycastToGroundEventHandler();
        public RaycastToGroundEventHandler RaycastToGround;

        public delegate bool OnClickEventHandler();
        public OnClickEventHandler OnClick;

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
#if UNITY_WEBGL || UNITY_EDITOR
            RaycastToGround += RaycastMouse;
            OnClick += ClickMouse;
#elif UNITY_ANDROID
            RaycastToGround += RaycastTouch;
            OnClick += ClickTouch;

#endif
        }

        private bool ClickMouse() {
            return Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject();
        }

        private bool ClickTouch() {
            if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
                
                return !EventSystem.current.IsPointerOverGameObject(touch.fingerId) && touch.phase == TouchPhase.Ended;
            }
            return false;
        }

        private Ray RaycastMouse() {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        private Ray RaycastTouch() {
            if (!isMoving && Input.touchCount >= 1) {
                Touch touch = Input.GetTouch(0);
                return Camera.main.ScreenPointToRay(touch.position);
            }
            return new Ray();
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

#if UNITY_ANDROID 
            if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase) {
                    case TouchPhase.Began:
                        startPos = touch.position;
                        break;
                    case TouchPhase.Moved:
                        isMoving = true;
                        break;
                    case TouchPhase.Ended:
                        isMoving = false;
                        break;
                }

                if (touch.phase == TouchPhase.Began) {
                    startPos = touch.position;
                }

                if (touch.phase == TouchPhase.Moved) {
                    isMoving = true;

                }

                Ray ray = Camera.main.ScreenPointToRay(startPos);
                RaycastHit hit;
                bool raycast = Physics.Raycast(ray, out hit, 100, groundMask);

                if ((touch.position - startPos).magnitude > 45) {
                    axis = (-touch.deltaPosition.x / 2, -touch.deltaPosition.y / 2);
                }
                
            }
#endif
            
            OnMove?.Invoke(this);
            return axis;
        }

        private bool isMoving = false;
        private Vector2 startPos = Vector2.zero;


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