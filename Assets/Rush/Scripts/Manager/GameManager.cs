///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 05/11/2019 14:25
///-----------------------------------------------------------------

using Com.IsartDigital.Rush.Tiles;
using Com.IsartDigital.Rush.Ui;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Com.IsartDigital.Rush.Manager {
    public class GameManager : MonoBehaviour {
        private static GameManager instance;
        [SerializeField] GameObject winScreen;
        [SerializeField] GameObject levelSelector;
        public static GameManager Instance { get { return instance; } }

        public static List<Cube> cubeList;

        public bool isInActionPhase { get; private set; } = false;
        private bool isLost = false;
        private bool win = false;

        public bool isPause { get; private set; } = false;

        public event Action<bool> onSwitchPhase;

        public event Action OnActionPhase;
        public event Action OnReflexionPhase;

        public delegate void PlayPauseEventHandler(bool isOn);
        public event PlayPauseEventHandler GameIsPaused;

        private bool isInit = false;

        private void Awake() {
            if (instance) {
                Destroy(gameObject);
                return;
            }

            instance = this;
            
            Cube.LooseCondition += Loose;
        }

        private void Start() {
            HudManager.Instance.AddScreen(levelSelector);

        }


        public void Init() {
            TimeManager.Instance.Init();

            Hud.Instance.PlayPause += PlayPauseGame;
            Hud.Instance.SwitchPhase += SwitchMode;
            isInit = true;
            PlayPauseGame(true);
        }

        private void UnInit() {
            win = isLost = isInit = isPause = false;

            TimeManager.Instance.UnInit();
            Hud.Instance.PlayPause -= PlayPauseGame;
            Hud.Instance.SwitchPhase -= SwitchMode;

            Player.Instance.UnIinit();

        }

        public void OnBackToLevelSelector() {
            UnInit();
            if (isInActionPhase) {
                SwitchMode();
            }
            Hud.Instance.Reset();
            Cube.DestroyAll();
        }

        

        public void PlayPauseGame(bool isOn) {
            Time.timeScale = isOn ? 1 : 0;
            isPause = !isOn;

            GameIsPaused?.Invoke(isPause);
        }

        public void SwitchMode() {
            if (isPause) {
                return;
            }
            
            isInActionPhase = !isInActionPhase;

            if (!isInActionPhase) {
                SwitchToReflexionPhase();
            } else {
                SwitchToActionPhase();
            }

            isLost = false;
        }

        private void SwitchToReflexionPhase() {
            Turnstile.ResetAll();
            Spawner.ResetAll();
            Cube.DestroyAll();

            OnReflexionPhase();
        }

        private void SwitchToActionPhase() {
            OnActionPhase();
        }

        public void Loose() {
            Debug.Log("<color=red><size=21>GameOver</size></color>");
            OnReflexionPhase();
            isLost = true;
        }

        private void Win() {
            Debug.Log("<color=green><size=21>WIN</size></color>");
            HudManager.Instance.AddScreen(winScreen);
            HudManager.Instance.RemoveScreen(Hud.Instance.gameObject);
            PlayPauseGame(true);
            win = true;

        }
        private void OnDestroy() {
            if (this == instance) instance = null;
            Cube.LooseCondition -= Loose;
        }

        private void Update() {
            if (win || isLost || !isInit) {
                return;
            }

            if (Spawner.AllSpawnedAllCube() && Cube.list.Count == 0) {
                Win();
            }
        }
    }
}