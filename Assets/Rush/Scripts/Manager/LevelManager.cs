///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 14/11/2019 21:56
///-----------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace Com.IsartDigital.Rush.Manager {
	public class LevelManager : MonoBehaviour {

        [SerializeField] List<GameObject> levels = new List<GameObject>();
        [SerializeField] GameObject levelSelector;


		private static LevelManager instance;
		public static LevelManager Instance { get { return instance; } }
		
		private void Awake(){
			if (instance){
				Destroy(gameObject);
				return;
			}
			
			instance = this;
		}

        private void Start() {
            levelSelector.GetComponent<LevelSelectorManager>().OnLoadLevel += LoadLevel;
        }


        public void LoadLevel(int index) {
            GameObject level = levels[index];

            level.SetActive(true);
            HudManager.Instance.Init(level);
            GameManager.Instance.Init();
            Player.Instance.Init(level);
        }

        public void UnloadLevels() {
            for (int i = levels.Count - 1; i >= 0; i--) {
                levels[i].SetActive(false);
            }
        }

		
		private void OnDestroy(){
			if (this == instance) instance = null;
		}
	}
}