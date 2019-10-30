///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 21/10/2019 14:03
///-----------------------------------------------------------------

using System;
using UnityEngine;

namespace Com.IsartDigital.Rush {
	public class TimeManager : MonoBehaviour {
        private static TimeManager instance;

        [SerializeField, Range(0f, 5f)] public float speed = 1;

        private float elapsedTime = 0;
        private float durationBetweenTicks = 1;
        private float _ratio;
        public float Ratio { get => _ratio; }

        public Action OnTick;

        public static TimeManager Instance { get { return instance; } }

        private void Awake() {
            if (instance) {
                Destroy(gameObject);
                return;
            }

            instance = this;
        }

        private void Start () {
			
		}
		
		private void Update () {
            Tick();
            
		}

        private void Tick() {

            if (elapsedTime > durationBetweenTicks) {
                Debug.Log("<color=green><size=21>Tick</size></color>");
                OnTick?.Invoke();
                elapsedTime = 0;

            }

            elapsedTime += Time.deltaTime * speed;

            _ratio = Mathf.Clamp01(elapsedTime / durationBetweenTicks);

        }
        private void OnDestroy() {

        }
    }
}