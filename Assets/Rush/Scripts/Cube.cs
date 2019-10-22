///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 21/10/2019 12:56
///-----------------------------------------------------------------

using System;
using UnityEngine;

namespace Com.IsartDigital.Rush {
    public class Cube : MonoBehaviour {


        [SerializeField, Range(0.1f, 5f)] private float speed = 0.7f;

        private float elapsedTime = 0;
        private float durationBetweenTicks = 1;
        private float ratio;

        private Vector3 fromPosition;
        private Vector3 toPosition;
        private Quaternion fromRotation;
        private Quaternion toRotation;

        private Vector3 movementDirection;
        private Quaternion movementRotation;

        private float rotationOffsetY = 0f;
        private float cubeSide = 1f;
        private float cubeFaceDiagonal = 0f;

        private Action doAction;

        private void Start() {
            cubeFaceDiagonal = Mathf.Sqrt(2) * cubeSide;
            rotationOffsetY = cubeFaceDiagonal / 2 - cubeSide / 2;

            movementDirection = transform.forward;
            movementRotation = Quaternion.AngleAxis(90f, transform.right);
            SetModeVoid();
        }

        private void Update() {
            Tick();
            doAction();
        }

        private void Tick() {
            if (elapsedTime > durationBetweenTicks) {
                Debug.Log("Tick");
                elapsedTime = 0;

                SetModeMove();

            }

            elapsedTime += Time.deltaTime * speed;
            ratio = elapsedTime / durationBetweenTicks;

        }

        private void SetModeVoid() {
            doAction = doActionVoid;
        }

        private void doActionVoid() {

        }

        private void InitNextMove() {
            fromPosition = transform.position;
            fromRotation = transform.rotation;

            toPosition = fromPosition + movementDirection;
            toRotation = movementRotation * fromRotation;
        }

        private void SetModeMove() {
            InitNextMove();
            doAction = DoActionMove;
        }

        private void DoActionMove() {
            transform.position = Vector3.Lerp(fromPosition, toPosition, ratio) + (Vector3.up * (rotationOffsetY * Mathf.Sin(Mathf.PI * ratio)));
            transform.rotation = Quaternion.Lerp(fromRotation, toRotation, ratio);
        }
    }
}