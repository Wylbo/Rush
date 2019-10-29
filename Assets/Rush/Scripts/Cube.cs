///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 21/10/2019 12:56
///-----------------------------------------------------------------

using Com.IsartDigital.Rush.Tiles;
using System;
using UnityEngine;

namespace Com.IsartDigital.Rush {
    public class Cube : MonoBehaviour {

        [SerializeField] AnimationCurve moveCurve;
        [SerializeField] Light light;

        private Vector3 fromPosition;
        private Vector3 toPosition;
        private Quaternion fromRotation;

        private Quaternion toRotation;


        public Vector3 movementDirection { get; private set; }
        private Quaternion movementRotation;

        private float rotationOffsetY = 0f;
        private float cubeSide = 1f;
        private float cubeFaceDiagonal = 0f;

        private float raycastDistance = 0;
        private float raycastOffsetDistance = 0.4f;
        private Vector3 down;
        private RaycastHit hit;

        private Vector3 forward;


        public int nTickToWait { get; set; }
        public bool isWaiting { get; private set; }
        public bool isConvoyed { get; private set; }
        private int tickCounter = 0;
        private Transform tpTarget;


        private string groundTag = "Ground";
        private string tileTag = "Tile";

        private Action doAction;

        private void Start() {
            TimeManager.Instance.OnTick += Tick;

            raycastDistance = cubeSide / 2 + raycastOffsetDistance;

            cubeFaceDiagonal = Mathf.Sqrt(2) * cubeSide;
            rotationOffsetY = cubeFaceDiagonal / 2 - cubeSide / 2;

            movementDirection = transform.forward;
            movementRotation = Quaternion.AngleAxis(90f, transform.right);

            toPosition = transform.position;
            toRotation = transform.rotation;

            light.color = GetComponent<Renderer>().sharedMaterial.color;

            

            SetModeVoid();


        }

        private void CheckCollision() {
            down = Vector3.down;
            forward = movementDirection;

            if (Physics.Raycast(transform.position, down, out hit, raycastDistance)) {
                GameObject hitObject = hit.collider.gameObject;

                if (Physics.Raycast(transform.position, forward, out hit, raycastDistance)) {
                    GameObject hitObjectInFront = hit.collider.gameObject;

                    if (hitObjectInFront.CompareTag(groundTag)) {
                        SetDirection(Vector3.Cross(Vector3.up, movementDirection));
                        SetModeWait(0);
                        return;

                    }
                }
                SetModeMove();
            } else {
                SetModeFall();
            }
        }

        private void Update() {
            doAction();
        }

        private void Tick() {
            if (isWaiting || isConvoyed) {
                tickCounter++;
                return;
            }

            CheckCollision();


        }

        private void SetModeVoid() {
            doAction = doActionVoid;
        }

        private void doActionVoid() {

        }

        public void SetDirection(Vector3 newDirection) {
            movementDirection = newDirection;
            movementRotation = Quaternion.AngleAxis(90f, Vector3.Cross(Vector3.up, movementDirection));
        }

        private void InitNextMove() {
            fromPosition = toPosition;
            fromRotation = toRotation;

            toPosition = fromPosition + movementDirection;
            toRotation = movementRotation * fromRotation;
        }

        private void SetModeMove() {
            InitNextMove();
            doAction = DoActionMove;
        }

        private void DoActionMove() {
            transform.position = Vector3.Lerp(fromPosition, toPosition, moveCurve.Evaluate(TimeManager.Instance.Ratio))
                + Vector3.up * rotationOffsetY * Mathf.Sin(Mathf.PI * Mathf.Clamp01(moveCurve.Evaluate(TimeManager.Instance.Ratio)));
            transform.rotation = Quaternion.Lerp(fromRotation, toRotation, moveCurve.Evaluate(TimeManager.Instance.Ratio));
        }

        private void InitNextFall() {
            fromPosition = transform.position;

            toPosition = fromPosition + Vector3.down;
        }
        private void SetModeFall() {
            InitNextFall();
            doAction = doActionFall;
        }

        private void doActionFall() {
            transform.position = Vector3.Lerp(fromPosition, toPosition, TimeManager.Instance.Ratio);
        }

        public void SetModeWait(int nTickToWait) {
            isWaiting = true;
            this.nTickToWait = nTickToWait;
            doAction = doActionWait;
        }

        private void doActionWait() {
            if (tickCounter > nTickToWait) {
                CheckCollision();
                tickCounter = 0;
                isWaiting = false;
            }
        }

        private void InitNextConvoyedMovement(Vector3 convoyeurDirection) {
            fromPosition = toPosition;
            toPosition = fromPosition + convoyeurDirection;
        }

        public void SetModeConvoyed(Vector3 convoyeurDirection) {
            isConvoyed = true;
            InitNextConvoyedMovement(convoyeurDirection);
            doAction = DoActionConveyed;
        }

        private void DoActionConveyed() {

            transform.position = Vector3.Lerp(fromPosition, toPosition, TimeManager.Instance.Ratio);

            if (TimeManager.Instance.Ratio >= 1) {
                tickCounter = 0;
                if (Physics.Raycast(transform.position, down, out hit, raycastDistance)) {
                    GameObject hitObject = hit.collider.gameObject;
                    if (hitObject.CompareTag(groundTag)) {
                        SetModeWait(1);
                    }
                }
                isConvoyed = false;
            }

        }

        public void SetModeTeleport(Transform target) {
            isWaiting = true;
            doAction = DoActionTeleport;
            tpTarget = target;
        }

        private void DoActionTeleport() {
            if (tickCounter > 1) {
                isWaiting = false;

                transform.position = tpTarget.position + Vector3.up / 2;
                toPosition = transform.position;

                SetModeWait(2);
            }
        }

        private void OnDestroy() {
            TimeManager.Instance.OnTick -= Tick;
        }
    }
}