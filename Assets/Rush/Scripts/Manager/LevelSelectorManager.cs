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
		private void Start () {
            for (int i = list.Count - 1; i >= 0; i--) {
                list[i].GetComponent<LevelButton>().OnButtonClick += LoadLevel;
            }
		}

        private void LoadLevel(GameObject level) {
            level.SetActive(true);
            HudManager.Instance.Init(level);
            GameManager.Instance.Init();
            transform.parent.gameObject.SetActive(false);
            Player.Instance.Init(level);

        }

        private void Update () {
			
		}
	}
}