///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 14/11/2019 20:14
///-----------------------------------------------------------------

using System;
using UnityEngine;
using UnityEngine.UI;

namespace Com.IsartDigital.Rush.Ui.LevelSelector {
	public class LevelButton : MonoBehaviour {

        [SerializeField] private GameObject level;
        private Button button;

        public event Action<GameObject> OnButtonClick;
        
		private void Start () {
            button = GetComponent<Button>();
            button.onClick.AddListener(() => OnButtonClick(level));
        }
		
        private void _LoadLevel() {
        }


	}
}