///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 21/10/2019 12:56
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.IsartDigital.Rush {
	public class Cube : MonoBehaviour {

        public Vector3 NextDirection;

        private Vector3 Direction;

        private Vector3 CurrentPos;
        private Vector3 NextPos;


        private void Start () {
            NextDirection = transform.forward;
            Direction = transform.forward;
            CurrentPos = transform.position;
            NextPos = transform.position + Direction;
		}
		
		private void Update () {
            MoveForward();

            if (transform.position == NextPos) {
                Direction = NextDirection;
                NextPos = transform.position + Direction;
                Debug.Log("hey");

            }
        }


        private void MoveForward() {
            transform.position += Direction * Time.deltaTime;
            //transform.Rotate(Vector3.Cross(Vector3.up,Direction),90 * Time.deltaTime);

        }


    }
}