///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 05/11/2019 10:59
///-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Com.IsartDigital.Rush {

    public enum Orientation : int {
        FORWARD = 0,
        BACKWARD = 180,
        RIGHT = 90,
        LEFT = -90
    };

    [Serializable]
    public class ElementInventory {
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private Orientation _direction;
        [SerializeField] private int number;

        public List<GameObject> Tiles { get; set; }

        public int Number { get { return number; } }
        public GameObject Tile { get { return tilePrefab; } }
        public Quaternion Direction {
            get { return Quaternion.AngleAxis((int)_direction, Vector3.up); }
        }

        public void Init() {
            Tiles = new List<GameObject>();
            for (int i = 0; i < number; i++) {
                Tiles.Add(tilePrefab);
            }
        }

        public bool CompareType(GameObject toCompare) {
            return toCompare.tag == tilePrefab.tag && toCompare.transform.rotation == Direction;
        }

        public void AddOneToList() {
            Tiles.Add(tilePrefab);
        }
    }
}