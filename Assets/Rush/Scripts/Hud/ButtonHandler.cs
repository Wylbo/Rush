///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 11/11/2019 18:21
///-----------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

namespace Com.IsartDigital.Rush.Hud {
	public class ButtonHandler : MonoBehaviour {

        public int index;

        private Button button;

		private void Start () {
            button = GetComponent<Button>();
            button.onClick.AddListener(() => GetIndex());
		}
		
        public void GetIndex() {
            Debug.Log(index);
        }
	}
}