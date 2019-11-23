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
        private Image image;
        [SerializeField] private Sprite imageOff;
        private Sprite imageOn;

        public Action<int> OnClick;

        private void Awake() {
            image = GetComponent<Image>();
            image.color = Color.black;
            imageOn = image.sprite;


        }

        private void Start() {
            button = GetComponent<Button>();
            button.onClick.AddListener(GetIndex);
            image = GetComponent<Image>();
        }

        public void GetIndex() {
            OnClick(index);
            //Player.Instance.OnHudButtonClick(index);
        }

        public void lightOnOff(bool islight, Color color) {

            image.sprite = islight ? imageOn : imageOff;
            image.color = color;
        }
    }
}