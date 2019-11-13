///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 05/11/2019 14:25
///-----------------------------------------------------------------

using Com.IsartDigital.Rush.Tiles;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Com.IsartDigital.Rush.Manager {
    public class GameManager : MonoBehaviour {
        private static GameManager instance;
        public static GameManager Instance { get { return instance; } }

        public static List<Cube> cubeList;

        private bool isInActionPhase = false;
        private bool isLost = false;
        private bool win = false;

        public Action onPausePlay;

        private void Awake() {
            if (instance) {
                Destroy(gameObject);
                return;
            }

            instance = this;
            
            Cube.LooseCondition += Loose;
        }

        

        private void Start() {
            TimeManager.Instance.Init();
            Hud.Hud.Instance.Init();
        }

        public void SwitchMode() {
            isInActionPhase = !isInActionPhase;

            if (!isInActionPhase) {
                Turnstile.ResetAll();
                Spawner.ResetAll();
                Cube.DestroyAll();
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

        private void Win() {
            Debug.Log("<color=green><size=21>WIN</size></color>");
            win = true;

        }
        private void OnDestroy() {
            if (this == instance) instance = null;
            Cube.LooseCondition -= Loose;
        }

        private void Update() {
            if (win || isLost) {
                return;
            }

            if (Spawner.AllSpawnedAllCube() && Cube.list.Count == 0) {
                Win();
            }
        }
    }
}