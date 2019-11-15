///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 15/11/2019 00:54
///-----------------------------------------------------------------

using System;
using UnityEngine;
using UnityEngine.UI;

namespace Com.IsartDigital.Rush.Ui {
	public class ToggleBtn : MonoBehaviour {
        [SerializeField] private Sprite spriteIsOff;
        protected Sprite spriteIsOn;
        protected Image image;
        protected Toggle toggle;
        protected virtual void Start () {
            toggle = GetComponent<Toggle>();
            image = toggle.image;
            spriteIsOn = image.sprite;
            toggle.onValueChanged.AddListener(toggle_onValueChanged);
        }

        protected virtual void toggle_onValueChanged(bool isOn) {
            changeSprite(isOn);
        }

        protected virtual void changeSprite(bool isOn) {
            image.sprite = !isOn ? spriteIsOn : spriteIsOff;
        }
    }
}