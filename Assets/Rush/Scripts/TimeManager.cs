///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 21/10/2019 14:03
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.IsartDigital.Rush {
	public class TimeManager : MonoBehaviour {

        private float _timeScale;
        [Range(0,5)]public float TimeScale;
            //get {
            //    return _timeScale;
            //}
            //set {
            //    _timeScale = value;
            //    Time.timeScale = value;
            //    //OnTimeScaleChange?.Invoke();
            //}
        

        public delegate void TimeManagerEventHandler();
        public event TimeManagerEventHandler OnTimeScaleChange;

		private void Start () {
			
		}
		
		private void Update () {
            Time.timeScale = TimeScale;
		}

        private void OnDestroy() {
            OnTimeScaleChange = null;
        }
    }
}