///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 08/11/2019 10:24
///-----------------------------------------------------------------

using Com.IsartDigital.Rush.Tiles;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.IsartDigital.Rush.Ui {
    public class Hud : MonoBehaviour {

        [SerializeField] private RectTransform tileButtonContainer;
        [SerializeField] private GameObject level;
        [SerializeField] private GameObject uiButton;
        [SerializeField] private Button playPauseButton;
        [SerializeField] private Button switchPhaseButton;

        private List<ElementInventory> levelInventory;
        private List<GameObject> buttonList = new List<GameObject>();

        public event Action<bool> PlayPause;
        public event Action SwitchPhase;


        private static Hud instance;
        public static Hud Instance { get { return instance; } }

        private void Awake() {
            if (instance) {
                Destroy(gameObject);
                return;
            }

            instance = this;
        }

        public void Reset() {

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

            Player.Instance.OnElementPlaced += UpdateHud;

            levelInventory = level.GetComponent<Level>().list;

            GameObject uiTile;
            GameObject button;
            playPauseButton.GetComponent<PlayPauseBtn>().isOn = true;
            playPauseButton.GetComponent<PlayPauseBtn>().OnClick += PlayPauseToggle_OnValueChanged;
            switchPhaseButton.GetComponent<SwitchPhaseBtn>().OnClick += SwitchPhaseToggle_OnValueChanged;

            for (int i = 0; i < levelInventory.Count; i++) {
                button = Instantiate(uiButton, tileButtonContainer, false);
                button.transform.localScale = Vector3.one;
                button.GetComponentInChildren<Text>().text = levelInventory[i].Number.ToString();
                button.GetComponent<ButtonHandler>().index = i;
                buttonList.Add(button);
                button.GetComponent<ButtonHandler>().OnClick += Player.Instance.OnHudButtonClick;

                uiTile = Instantiate(levelInventory[i].Tile.GetComponent<ADraggableTile>().UiTile, button.transform,false);
                uiTile.transform.position += Vector3.back;
                uiTile.transform.localScale *= 100;

                uiTile.GetComponent<UiTile>().baseRotation = levelInventory[i].Direction;


            }
        }

        private void UpdateHud(int ntile, int index) {
            buttonList[index].GetComponentInChildren<Text>().text = ntile.ToString();
            if (ntile == 0) {
                buttonList[index].SetActive(false);
            } else {
                buttonList[index].SetActive(true);
            }
        }

        private void SwitchPhaseToggle_OnValueChanged() {
            SwitchPhase?.Invoke();
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