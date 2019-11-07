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
        private bool isLost = false;

        public Action onPausePlay;

        private void Awake() {
            if (instance) {
                Destroy(gameObject);
                return;
            }

            instance = this;
            TimeManager.Instance.Init();

            Cube.HitAnOtherCube += Loose;
        }

        private void Start() {

        }

        private void Update() {

        }

        public void SwitchMode() {

            isInActionPhase = !isInActionPhase;


            if (!isInActionPhase) {
                Cube.DestroyAll();
                Spawner.ResetAll();
                if (isLost) {
                }
            }
            if (!isLost) {
                onPausePlay();
            }

            isLost = false;
        }

        public void Loose() {
            Debug.Log("<color=red><size=21>GameOver</size></color>");
            onPausePlay();
            isLost = true;
        }

        private void OnDestroy() {
            if (this == instance) instance = null;
            Cube.HitAnOtherCube -= Loose;
        }
    }
}