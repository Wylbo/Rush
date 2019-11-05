///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 05/11/2019 10:59
///-----------------------------------------------------------------

using System;
using UnityEngine;

namespace Com.IsartDigital.Rush {

    public enum Orientation {
        FORWARD,
        BACKWARD,
        RIGHT,
        LEFT
    };

    [Serializable]
    public class ElementInventory {
        [SerializeField] private GameObject tile;
        [SerializeField] private Orientation direction;
        public GameObject Tiles { get { return tile; } }

        public Quaternion Direction {
            get {
                if (direction == Orientation.RIGHT) return Quaternion.AngleAxis(90, Vector3.up);
                else if (direction == Orientation.LEFT) return Quaternion.AngleAxis(-90, Vector3.up);
                else if (direction == Orientation.BACKWARD) return Quaternion.AngleAxis(180, Vector3.up);
                else return Quaternion.identity;
            }
        }


        private void Start() {

        }

        private void Update() {

        }
    }
}