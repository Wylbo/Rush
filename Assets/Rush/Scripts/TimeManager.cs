///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 21/10/2019 14:03
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.IsartDigital.Rush {
	public class TimeManager : MonoBehaviour {

        private float _timeScale;
        [Range(0,5)] public float TimeScale;
        

		private void Start () {
			
		}
		
		private void Update () {
            //Time.timeScale = TimeScale;
		}

        private void OnDestroy() {

        }
    }
}