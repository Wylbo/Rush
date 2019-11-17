///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 11/11/2019 18:21
///-----------------------------------------------------------------

using System;
using UnityEngine;
using UnityEngine.UI;

namespace Com.IsartDigital.Rush.Ui {
    public class ButtonHandler : MonoBehaviour {

        public int index;
        private Button button;

        public Action<int> OnClick;

        private void Start() {
            button = GetComponent<Button>();
            button.onClick.AddListener(GetIndex);

        }

        public void GetIndex() {
            OnClick(index);
            //Player.Instance.OnHudButtonClick(index);
        }
    }
}