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
        [SerializeField] AudioSource click;

        public event Action<int> OnLoadLevel;
		private void Start () {
            for (int i = list.Count - 1; i >= 0; i--) {
                list[i].GetComponent<LevelButton>().OnButtonClick += LoadLevel;
            }
		}

        private void LoadLevel(int levelToLoad) {
            OnLoadLevel(levelToLoad);
            click.Play();
            HudManager.Instance.RemoveScreen(gameObject);
        }
	}
}