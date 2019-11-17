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
        protected Button button;
        public bool isOn = false;
        
        protected virtual void Start () {
            button = GetComponent<Button>();
            image = GetComponent<Image>();
            spriteIsOn = image.sprite;
            button.onClick.AddListener(button_onClick);
        }


        protected virtual void button_onClick() {
            isOn = !isOn;
            changeSprite(isOn);
        }

        protected virtual void changeSprite(bool isOn) {
            image.sprite = !isOn ? spriteIsOn : spriteIsOff;
        }
    }
}