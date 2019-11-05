///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 05/11/2019 14:25
///-----------------------------------------------------------------

using Com.IsartDigital.Rush.Tiles;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Com.IsartDigital.Rush {
    public class GameManager : MonoBehaviour {
        private static GameManager instance;
        public static GameManager Instance { get { return instance; } }

        public static List<Cube> cubeList;

        private bool isInActionPhase = false;

        public Action onPlay;

        private void Awake() {
            if (instance) {
                Destroy(gameObject);
                return;
            }

            instance = this;
        }

        private void Start() {

        }

        private void Update() {

        }

        public void SwitchMode() {

            isInActionPhase = !isInActionPhase;
            Debug.Log(isInActionPhase);

            if (!isInActionPhase) {
                Cube.DestroyAll();
                Spawner.ResetAll();
                Debug.Log("<size=18>merde</size>");
            }

            onPlay();

        }

        private void OnDestroy() {
            if (this == instance) instance = null;
        }
    }
}