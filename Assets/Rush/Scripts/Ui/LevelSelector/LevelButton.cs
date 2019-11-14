///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 14/11/2019 20:14
///-----------------------------------------------------------------

using System;
using UnityEngine;
using UnityEngine.UI;

namespace Com.IsartDigital.Rush.Ui.LevelSelector {
	public class LevelButton : MonoBehaviour {

        [SerializeField] private int levelToLoad;
        private Button button;

        public event Action<int> OnButtonClick;
        
		private void Start () {
            button = GetComponent<Button>();
            button.onClick.AddListener(() => OnButtonClick(levelToLoad));
        }
		
        private void _LoadLevel() {
        }


	}
}