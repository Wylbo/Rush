///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 21/10/2019 12:56
///-----------------------------------------------------------------

using Com.IsartDigital.Rush.Tiles;
using System;
using UnityEngine;

namespace Com.IsartDigital.Rush {
    public class Cube : MonoBehaviour {

        private Vector3 fromPosition;
        private Vector3 toPosition;
        private Quaternion fromRotation;

        private Quaternion toRotation;

        public int nTickToWait { get; set; }

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


        public bool isWaiting {get; private set; }
        public bool isConvoyed { get; private set; }
        private int tickCounter = 0;

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
                        SetModeWait(1);
                        SetDirection(Vector3.Cross(Vector3.up, movementDirection));
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
            transform.position = Vector3.Lerp(fromPosition, toPosition, TimeManager.Instance.Ratio)
                + Vector3.up * rotationOffsetY * Mathf.Sin(Mathf.PI * Mathf.Clamp01(TimeManager.Instance.Ratio));
            transform.rotation = Quaternion.Lerp(fromRotation, toRotation, TimeManager.Instance.Ratio);
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
            Debug.Log(fromPosition);
            Debug.Log(toPosition);
        }

        public void SetModeConvoyed(Vector3 convoyeurDirection) {
            isConvoyed = true;
            InitNextConvoyedMovement(convoyeurDirection);
            doAction = DoActionConveyed;


            Debug.Log("<color=red>hey2</color>");
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
    }
}