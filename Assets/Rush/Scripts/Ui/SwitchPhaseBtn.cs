///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 14/11/2019 16:28
///-----------------------------------------------------------------

using Com.IsartDigital.Rush.Manager;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Com.IsartDigital.Rush.Ui {
	public class SwitchPhaseBtn : MonoBehaviour {

        [SerializeField] private Sprite spriteIsOff;
        private Sprite spriteIsOn;
        private Image image;
        private Toggle toggle;

        public delegate void SwitchPhaseEventHandler();
        public event SwitchPhaseEventHandler OnClick;

		private void Start () {
            toggle = GetComponent<Toggle>();
            image = toggle.image;
            spriteIsOn = image.sprite;
            toggle.onValueChanged.AddListener(toggle_onValueChanged);
            GameManager.Instance.onSwitchPhase += changeSprite;
        }

        private void changeSprite(bool isOn) {
            image.sprite = !isOn ? spriteIsOn : spriteIsOff;
        }

        private void toggle_onValueChanged(bool isOn) {
            changeSprite(isOn);
            OnClick();
            toggle.gameObject.SetActive(false);
            toggle.gameObject.SetActive(true);
        }


	}
}