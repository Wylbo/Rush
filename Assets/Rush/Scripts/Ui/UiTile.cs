///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 11/11/2019 14:28
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.IsartDigital.Rush.Ui {
	public class UiTile : MonoBehaviour {

        private Quaternion baseRotation;
		private void Start () {
            baseRotation = transform.rotation;
		}
		
		private void Update () {
            CameraMove cam = Camera.main.GetComponent<CameraMove>();
            Vector3 toPivot = cam.toPivot;
            toPivot = Vector3.ProjectOnPlane(toPivot, Vector3.up);

            Debug.DrawRay(Camera.main.transform.position, toPivot, Color.green);

            toPivot = baseRotation  *  toPivot;
            Debug.DrawRay(transform.position, toPivot * 100, Color.red);

            transform.rotation = Quaternion.LookRotation(toPivot, Vector3.forward);
        }
    }
}