///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 21/10/2019 14:03
///-----------------------------------------------------------------

using Com.IsartDigital.Rush.Ui;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Com.IsartDigital.Rush.Manager {
    public class TimeManager : MonoBehaviour {
        private static TimeManager instance;

        [SerializeField, Range(0f, 5f)] private float speed = 1;
        //[SerializeField] Slider slider;

        public float tickRate;
        private bool isTicking = false;
        private float elapsedTime = 0;
        private float durationBetweenTicks = 1;
        private float _ratio;
        public float Ratio { get => _ratio; }

        public event Action OnTick;

        public static TimeManager Instance { get { return instance; } }

        private void Awake() {
            if (instance) {
                Destroy(gameObject);
                return;
            }

            instance = this;

            //Hud.Instance.OnSliderMoved += UpdateTickRate;

        }

        public void UpdateTickRate(float value) {
            if (tickRate > 0) {
                tickRate = value;
            }
        }

        public void Init() {
            //GameManager.Instance.onSwitchPhase += onOff;
            GameManager.Instance.OnActionPhase += Activate;
            GameManager.Instance.OnReflexionPhase += Desactivate;
        }

        public void UnInit() {
            //GameManager.Instance.onSwitchPhase -= onOff;
            if (isTicking) {
                onOff(true);
            }
        }

        private void Activate() {
            isTicking = true;
            tickRate = speed;
        }

        private void Desactivate() {
            isTicking = false;
            tickRate = 0;
        }

        private void onOff(bool isOn) {
            isTicking = isOn;

            if (isTicking) {
                tickRate = speed;
            } else {
                tickRate = 0;
            }
        }

        private void Update() {
            Tick();
        }

        private void Tick() {
            if (elapsedTime > durationBetweenTicks) {
                Debug.Log("<color=green><size=21>Tick</size></color>");
                OnTick?.Invoke();
                elapsedTime = 0;

            }

            elapsedTime += Time.deltaTime * tickRate;

            _ratio = Mathf.Clamp01(elapsedTime / durationBetweenTicks);

        }
        private void OnDestroy() {
            if (this == instance) instance = null;
            OnTick = null;
        }
    }
}