///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 21/10/2019 12:56
///-----------------------------------------------------------------

using System;
using UnityEngine;

namespace Com.IsartDigital.Rush {
    public class Cube : MonoBehaviour {

        private Vector3 fromPosition;
        private Vector3 toPosition;
        private Quaternion fromRotation;

        private Quaternion toRotation;

        private Vector3 _movementDirection;
        public Vector3 MovementDirection { get => _movementDirection; }

        private Quaternion movementRotation;

        private float rotationOffsetY = 0f;
        private float cubeSide = 1f;
        private float cubeFaceDiagonal = 0f;

        private float raycastDistance = 0;
        private float raycastOffsetDistance = 0.4f;
        private Vector3 down;
        private RaycastHit hit;

        private bool isWaiting = false;

        private string groundTag = "Ground";



        private Action doAction;

        private void Start() {
            TimeManager.Instance.OnTick += Tick;

            raycastDistance = cubeSide / 2 + raycastOffsetDistance;

            cubeFaceDiagonal = Mathf.Sqrt(2) * cubeSide;
            rotationOffsetY = cubeFaceDiagonal / 2 - cubeSide / 2;

            _movementDirection = transform.forward;
            movementRotation = Quaternion.AngleAxis(90f, transform.right);

            toPosition = transform.position;
            toRotation = transform.rotation;

            SetModeVoid();
        }

        private void CheckCollision() {
            down = Vector3.down;

            if (Physics.Raycast(transform.position, down, out hit, raycastDistance)) {
                GameObject hitObject = hit.collider.gameObject;
                //Debug.Log(hitObject);
                if (hitObject.CompareTag(groundTag)) {
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
            if (doAction == doActionWait) {
                isWaiting = true;
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
            _movementDirection = newDirection;
            movementRotation = Quaternion.AngleAxis(90f, Vector3.Cross(Vector3.up, _movementDirection));
        }

        private void InitNextMove() {
            fromPosition = toPosition;
            fromRotation = toRotation;

            toPosition = fromPosition + _movementDirection;
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

        public void SetModeWait() {
            doAction = doActionWait;
        }

        private void doActionWait() {
            if (isWaiting) {
                SetModeMove();
                isWaiting = false;
            }
        }
    }
}