///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 14/11/2019 21:56
///-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Com.IsartDigital.Rush.Manager {
	public class LevelManager : MonoBehaviour {

        [SerializeField] List<GameObject> levelsPrefabsList = new List<GameObject>();
        [SerializeField] GameObject levelSelector;
        [SerializeField] Transform tileContainer;

        public event Action<GameObject> OnLevelLoading;
        public event Action OnLevelUnload;

        private GameObject level;
        private int indexLevel;


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
            indexLevel = index;
            level = Instantiate(levelsPrefabsList[index]);
            level.transform.position = Vector3.zero;

            level.GetComponent<Level>().Init();
            level.SetActive(true);

            OnLevelLoading(level);

            //HudManager.Instance.Init(level);
            //Player.Instance.Init(level);
        }

        public void UnloadLevels() {
            Destroy(level);
            OnLevelUnload();

        }

        public void RestartLevel() {
            UnloadLevels();
            LoadLevel(indexLevel);
        }


        private void OnDestroy(){
			if (this == instance) instance = null;
		}
	}
}