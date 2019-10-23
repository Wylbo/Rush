///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 22/10/2019 15:09
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.IsartDigital.Rush.Tiles {
	public class Turnstile : ATile {

        private bool isRight = true;
        protected override void SetCubeAction(Cube cube) {
            base.SetCubeAction(cube);
            Debug.Log("hey");

            if (isRight) {
                cube.SetDirection(Vector3.Cross(Vector3.up, cube.movementDirection));
            } else {
                cube.SetDirection(Vector3.Cross(cube.movementDirection, Vector3.up));
            }
            isRight = !isRight;

        }
    }
}