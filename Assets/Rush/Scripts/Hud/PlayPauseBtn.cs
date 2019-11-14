///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 14/11/2019 16:28
///-----------------------------------------------------------------

using System;
using UnityEngine;
using UnityEngine.UI;

namespace Com.IsartDigital.Rush.Hud {
	public class PlayPauseBtn : MonoBehaviour {

        [SerializeField] private Sprite spriteIsOff;
        private Sprite spriteIsOn;
        private Image image;

        public delegate void PlayPauseBtnEventHandler(bool isOn);
        public event PlayPauseBtnEventHandler OnClick;

		private void Start () {
            Toggle toggle = GetComponent<Toggle>();
            image = toggle.image;
            spriteIsOn = image.sprite;
            toggle.onValueChanged.AddListener(toggle_onValueChanged);
		}

        private void toggle_onValueChanged(bool isOn) {
            image.sprite = isOn ? spriteIsOn : spriteIsOff;
            OnClick(isOn);
        }


	}
}