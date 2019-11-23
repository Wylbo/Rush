///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 08/11/2019 10:24
///-----------------------------------------------------------------

using Com.IsartDigital.Rush.Manager;
using Com.IsartDigital.Rush.Tiles;
using Pixelplacement;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.IsartDigital.Rush.Ui {
    public class Hud : MonoBehaviour {

        [SerializeField] private RectTransform tileButtonContainer;
        [SerializeField] private GameObject uiButton;
        [SerializeField] private Button playPauseButton;
        [SerializeField] private Button switchPhaseButton;
        [SerializeField] private Slider sliderTime;

        private GameObject level;
        private List<ElementInventory> levelInventory;
        private List<GameObject> buttonList = new List<GameObject>();

        public event Action<bool> PlayPause;
        public event Action SwitchPhase;


        private static Hud instance;
        public static Hud Instance { get { return instance; } }
        public event Action<float> OnSliderMoved;

        private void Awake() {
            if (instance) {
                Destroy(gameObject);
                return;
            }

            instance = this;
            //sliderTime.OnMove(() =>OnSliderMoved());
        }

        private void Start() {
            sliderTime.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void OnSliderValueChanged(float value) {
            OnSliderMoved?.Invoke(value);
        }

        public void Reset() {
            OnSliderMoved -= TimeManager.Instance.UpdateTickRate;

            playPauseButton.GetComponent<PlayPauseBtn>().OnClick -= PlayPauseToggle_OnValueChanged;
            switchPhaseButton.GetComponent<SwitchPhaseBtn>().OnClick -= SwitchPhaseToggle_OnValueChanged;

            for (int i = buttonList.Count - 1; i >= 0; i--) {
                Destroy(buttonList[i]);
            }
        }

        public void Init(GameObject levelToLoad) {
            buttonList.Clear();
            ActivateSidePanel(true);

            level = levelToLoad;

            sliderTime.value = 1;
            OnSliderMoved += TimeManager.Instance.UpdateTickRate;
            Player.Instance.OnElementPlaced += UpdateHud;
            Player.Instance.NewElemInHand += SelectElem;

            levelInventory = level.GetComponent<Level>().list;

            GameObject uiTile;
            GameObject button;
            playPauseButton.GetComponent<PlayPauseBtn>().isOn = true;
            playPauseButton.GetComponent<PlayPauseBtn>().OnClick += PlayPauseToggle_OnValueChanged;
            switchPhaseButton.GetComponent<SwitchPhaseBtn>().OnClick += SwitchPhaseToggle_OnValueChanged;

            for (int i = 0; i < levelInventory.Count; i++) {
                button = Instantiate(uiButton, tileButtonContainer);
                button.transform.localScale = Vector3.one;
                button.GetComponentInChildren<Text>().text = levelInventory[i].Number.ToString();
                button.GetComponent<ButtonHandler>().index = i;
                buttonList.Add(button);
                button.GetComponent<ButtonHandler>().OnClick += Player.Instance.OnHudButtonClick;

                uiTile = Instantiate(levelInventory[i].Tile.GetComponent<ADraggableTile>().UiTile, button.transform, false);
                uiTile.transform.position += Vector3.back;
                uiTile.transform.localScale *= 100;

                uiTile.GetComponent<UiTile>().baseRotation = levelInventory[i].Direction;

                Color textColor = uiTile.GetComponentInChildren<Renderer>().material.color;
                textColor.a = 1;

                button.GetComponentInChildren<Text>().color = textColor;
            }

            SelectElem(0);
        }

        private void SelectElem(int index) {
            for (int i = buttonList.Count - 1; i >= 0; i--) {
                Tween.LocalScale(buttonList[i].transform, Vector3.one, 0.2f, 0f, Tween.EaseInOutBack);

                buttonList[i].GetComponent<ButtonHandler>().lightOnOff(false, Color.black);

            }

            Tween.LocalScale(buttonList[index].transform, Vector3.one * 1.5f, 0.2f, 0f, Tween.EaseInOutBack);
            Color lightColor = buttonList[index].GetComponentInChildren<Renderer>().material.color;
            lightColor.a = 1;
            buttonList[index].GetComponent<ButtonHandler>().lightOnOff(true, lightColor);
        }

        private void UpdateHud(int ntile, int index) {
            Text text = buttonList[index].GetComponentInChildren<Text>();
            text.text = ntile.ToString();
            //Tween.Shake(text.transform, text.rectTransform.position,Vector3.one * 1, 10, 0);

            if (ntile == 0) {
                buttonList[index].transform.GetChild(1).gameObject.SetActive(false);
                Tween.LocalScale(buttonList[index].transform, Vector3.one, 0.2f, 0f, Tween.EaseInOutBack);
                buttonList[index].GetComponent<ButtonHandler>().lightOnOff(false, Color.black);


            } else {
                buttonList[index].transform.GetChild(1).gameObject.SetActive(true);
            }
        }

        private void SwitchPhaseToggle_OnValueChanged() {
            SwitchPhase?.Invoke();
            OnSliderMoved?.Invoke(sliderTime.value);
        }

        private void PlayPauseToggle_OnValueChanged(bool isOn) {
            PlayPause?.Invoke(isOn);
            ActivateSidePanel(isOn);
        }

        private void ActivateSidePanel(bool isOn) {
            switchPhaseButton.enabled = isOn;
            switchPhaseButton.gameObject.SetActive(isOn);
            tileButtonContainer.gameObject.SetActive(isOn);
        }


        private void OnDestroy() {
            if (this == instance) instance = null;
            Player.Instance.OnElementPlaced -= UpdateHud;

        }

        public void returnIndex(int i) {
            Debug.Log(i);
        }
    }
}