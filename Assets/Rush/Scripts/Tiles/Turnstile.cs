///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 22/10/2019 15:09
///-----------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace Com.IsartDigital.Rush.Tiles {
	public class Turnstile : ADraggableTile {

        static private List<Turnstile> list { get; set; } = new List<Turnstile>();

        private bool isRight = true;
        private float rotationAngle;

        private void Awake() {
            list.Add(this);
        }

        private void Update() {
            rotationAngle = isRight ? 90 : -90;

            transform.rotation = Quaternion.AngleAxis(rotationAngle * Time.deltaTime, transform.up) * transform.rotation;
        }

        public override void SetCubeAction(Cube cube) {
            base.SetCubeAction(cube);
            if (isRight) {
                cube.SetDirection(Vector3.Cross(Vector3.up, cube.movementDirection));
            } else {
                cube.SetDirection(Vector3.Cross(cube.movementDirection, Vector3.up));
            }
            isRight = !isRight;

        }

        static public void ResetAll() {
            for (int i = list.Count - 1; i >= 0; i--) {
                list[i].Reset();
            }
        }

        private void Reset() {
            isRight = true;
        }
    }
}