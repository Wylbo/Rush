///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 14/11/2019 16:28
///-----------------------------------------------------------------

using Com.IsartDigital.Rush.Manager;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Com.IsartDigital.Rush.Ui {
	public class SwitchPhaseBtn : ToggleBtn {

        public delegate void SwitchPhaseEventHandler();
        public event SwitchPhaseEventHandler OnClick;

		override protected void Start () {
            base.Start();
            GameManager.Instance.OnSwitchPhase += changeSprite;
        }

        protected override void button_onClick() {
            base.button_onClick();
            OnClick?.Invoke();
        }


    }
}