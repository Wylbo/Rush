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

        public delegate void PlayPauseBtnEventHandler(bool isOn);
        public event PlayPauseBtnEventHandler OnClick;

		override protected void Start () {
            base.Start();
            GameManager.Instance.GameIsPaused += changeSprite;
		}

        protected override void changeSprite(bool isOn) {
            base.changeSprite(isOn);
            back.gameObject.SetActive(isOn);

        }

        override protected void toggle_onValueChanged(bool isOn) {
            base.toggle_onValueChanged(!isOn);
            OnClick(isOn);
        }


	}
}