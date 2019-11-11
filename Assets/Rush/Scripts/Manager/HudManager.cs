///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 11/11/2019 13:30
///-----------------------------------------------------------------

using UnityEngine;



namespace Com.IsartDigital.Rush.Manager {
	public class HudManager : MonoBehaviour {
		private static HudManager instance;
		public static HudManager Instance { get { return instance; } }
		
		private void Awake(){
			if (instance){
				Destroy(gameObject);
				return;
			}
			
			instance = this;
		}

        public void Init() {
            Hud.Hud.Instance.Init();
        }
		
		private void Start () {
			
		}
		
		private void Update () {
			
		}
		
		private void OnDestroy(){
			if (this == instance) instance = null;
		}
	}
}