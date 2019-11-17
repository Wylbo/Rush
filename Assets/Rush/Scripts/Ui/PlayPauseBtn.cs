///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 14/11/2019 16:28
///-----------------------------------------------------------------

using Com.IsartDigital.Rush.Manager;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Com.IsartDigital.Rush.Ui {
	public class PlayPauseBtn : ToggleBtn {

        [SerializeField] private Button back;
        [SerializeField] private Button reset;

        public delegate void PlayPauseBtnEventHandler(bool isOn);
        public event PlayPauseBtnEventHandler OnClick;

		override protected void Start () {
            base.Start();
            isOn = true;
            GameManager.Instance.GameIsPaused += changeSprite;
		}

        protected override void changeSprite(bool isOn) {
            base.changeSprite(isOn);
            back.gameObject.SetActive(isOn);
            reset.gameObject.SetActive(isOn);
        }

        protected override void button_onClick() {
            base.button_onClick();
            OnClick(isOn);
        }


    }
}