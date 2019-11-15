///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 14/11/2019 20:14
///-----------------------------------------------------------------

using Com.IsartDigital.Rush.Ui.LevelSelector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.IsartDigital.Rush.Manager {
	public class LevelSelectorManager : MonoBehaviour {

        [SerializeField] private List<Button> list = new List<Button>();

        public event Action<int> OnLoadLevel;
		private void Start () {
            for (int i = list.Count - 1; i >= 0; i--) {
                list[i].GetComponent<LevelButton>().OnButtonClick += LoadLevel;
            }
		}

        private void LoadLevel(int levelToLoad) {
            OnLoadLevel(levelToLoad);
            HudManager.Instance.RemoveScreen(gameObject);
        }

        private void Update () {
			
		}
	}
}