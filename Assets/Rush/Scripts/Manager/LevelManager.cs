///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 14/11/2019 21:56
///-----------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace Com.IsartDigital.Rush.Manager {
	public class LevelManager : MonoBehaviour {

        [SerializeField] List<GameObject> levelsPrefabsList = new List<GameObject>();
        [SerializeField] GameObject levelSelector;
        [SerializeField] Transform tileContainer;

        private GameObject level;


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
            level = Instantiate(levelsPrefabsList[index]);
            level.transform.position = Vector3.zero;

            level.GetComponent<Level>().Init();
            level.SetActive(true);
            HudManager.Instance.Init(level);
            GameManager.Instance.Init();
            Player.Instance.Init(level);
        }

        public void UnloadLevels() {
            Destroy(level);

        }


        private void OnDestroy(){
			if (this == instance) instance = null;
		}
	}
}