///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 11/11/2019 13:30
///-----------------------------------------------------------------

using Com.IsartDigital.Rush.Ui;
using UnityEngine;



namespace Com.IsartDigital.Rush.Manager {
	public class HudManager : MonoBehaviour {
		private static HudManager instance;
		public static HudManager Instance { get { return instance; } }

        [SerializeField] GameObject hud;

		private void Awake(){
			if (instance){
				Destroy(gameObject);
				return;
			}
			
			instance = this;
		}

        public void Init(GameObject level) {
            AddScreen(hud);
            hud.GetComponent<Hud>().Init(level);
        }

        public void AddScreen(GameObject screen) {
            screen.SetActive(true);
        }

        public void RemoveScreen(GameObject screen) {
            screen.SetActive(false);
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