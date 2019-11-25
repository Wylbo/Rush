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
        [SerializeField] GameObject TitleCard;
        [SerializeField] AudioSource menuMusic;
        public static GameManager Instance { get { return instance; } }

        public bool IsInActionPhase { get; private set; } = false;
        private bool isLost = false;
        private bool win = false;

        public bool IsPause { get; private set; } = false;

        public event Action<bool> OnSwitchPhase;

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

        }

        private void Start() {
            LevelManager.Instance.OnLevelLoading += Init;
            LevelManager.Instance.OnLevelUnload += UnInit;
            HudManager.Instance.AddScreen(TitleCard);

        }


        public void Init(GameObject level) {
            win = isLost = IsPause = false;

            TimeManager.Instance.Init();

            HudManager.Instance.Init(level);
            Hud.Instance.PlayPause += PlayPauseGame;
            Hud.Instance.SwitchPhase += SwitchMode;
            Hud.Instance.gameObject.GetComponent<Animator>().SetTrigger("Appear");
            Cube.OnLooseCondition += Loose;
            isInit = true;
            menuMusic.Stop();

            PlayPauseGame(true);
        }

        private void UnInit() {
            win = isLost = isInit = IsPause = false;

            TimeManager.Instance.UnInit();

            Hud.Instance.PlayPause -= PlayPauseGame;
            Hud.Instance.SwitchPhase -= SwitchMode;
            Cube.OnLooseCondition -= Loose;

            SwitchToReflexionPhase();

            menuMusic.Play();

            Player.Instance.UnIinit();

        }

        public void OnBackToLevelSelector() {
            UnInit();
            if (IsInActionPhase) {
                SwitchMode();
            }
            Cube.DestroyAll();
        }



        public void PlayPauseGame(bool isOn) {
            Time.timeScale = isOn ? 1 : 0;
            IsPause = !isOn;

            GameIsPaused?.Invoke(IsPause);
        }

        public void SwitchMode() {
            if (IsPause) {
                return;
            }


            if (IsInActionPhase) {
                SwitchToReflexionPhase();
                Hud.Instance.gameObject.GetComponent<Animator>().SetTrigger("Appear");
            } else {
                Hud.Instance.gameObject.GetComponent<Animator>().SetTrigger("Disappear");
                SwitchToActionPhase();
            }

            IsInActionPhase = !IsInActionPhase;


            isLost = false;
        }

        private void SwitchToReflexionPhase() {
            Turnstile.ResetAll();
            Spawner.ResetAll();
            Cube.DestroyAll();


            OnSwitchPhase(false);
            OnReflexionPhase();
        }

        private void SwitchToActionPhase() {
            OnActionPhase();
            OnSwitchPhase(true);
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
            UnInit();
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