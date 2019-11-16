///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 11/11/2019 14:28
///-----------------------------------------------------------------

using System;
using UnityEngine;

namespace Com.IsartDigital.Rush.Ui {
    public class UiTile : MonoBehaviour {

        public Quaternion baseRotation;

        private void Start() {
            //transform.rotation = Quaternion.AngleAxis(-90, Vector3.right) * baseRotation;
            CameraMove.Instance.OnMove += UpdateRotation;
        }

        private void UpdateRotation(CameraMove cam) {
             cam = Camera.main.GetComponent<CameraMove>();
            Vector3 toPivot = cam.toPivot;
            toPivot = Vector3.ProjectOnPlane(toPivot, Vector3.up);

            float angle = Vector3.SignedAngle(toPivot, Vector3.forward, Vector3.up);
            Quaternion finalRotation = Quaternion.AngleAxis(-45, Vector3.right) * Quaternion.AngleAxis(angle, Vector3.up) * baseRotation;
            transform.rotation = finalRotation;
        }

        private void OnDestroy() {
            CameraMove.Instance.OnMove -= UpdateRotation;
        }

    }
}