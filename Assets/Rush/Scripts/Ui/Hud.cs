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
        [SerializeField] private Toggle playPauseToggle;
        [SerializeField] private Toggle switchPhaseToggle;

        private List<ElementInventory> levelInventory;
        private List<GameObject> buttonList = new List<GameObject>();

        public event Action onButtonClick_handler;
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
            for (int i = 0; i < levelInventory.Count; i++) {
                button = Instantiate(uiButton, tileButtonContainer, false);
                button.transform.localScale = Vector3.one;
                button.GetComponentInChildren<Text>().text = levelInventory[i].Number.ToString();
                button.GetComponent<ButtonHandler>().index = i;
                buttonList.Add(button);
                button.GetComponent<ButtonHandler>().OnClick += Player.Instance.OnHudButtonClick;

                uiTile = Instantiate(levelInventory[i].Tile.GetComponent<ADraggableTile>().UiTile, button.transform,false);

                uiTile.transform.localScale *= 100;

                //uiTile.transform.rotation = levelInventory[i].Direction;
                //uiTile.transform.rotation = Quaternion.AngleAxis(-90, Vector3.right) * uiTile.transform.rotation;
                //uiTile.transform.rotation = Quaternion.AngleAxis(90, uiTile.transform.up) * uiTile.transform.rotation;
                uiTile.GetComponent<UiTile>().baseRotation = levelInventory[i].Direction;
                //uiTile.transform.rotation = levelInventory[i].Direction * uiTile.transform.rotation;

                playPauseToggle.GetComponent<PlayPauseBtn>().OnClick += PlayPauseToggle_OnValueChanged;
                switchPhaseToggle.GetComponent<SwitchPhaseBtn>().OnClick += SwitchPhaseToggle_OnValueChanged;

            }
        }

        private void UpdateHud(int ntile, int index) {
            buttonList[index].GetComponentInChildren<Text>().text = ntile.ToString();
        }

        private void SwitchPhaseToggle_OnValueChanged() {
            SwitchPhase();
        }

        private void PlayPauseToggle_OnValueChanged(bool isOn) {
            PlayPause(isOn);
            ActivateSidePanel(isOn);
        }

        private void ActivateSidePanel(bool isOn) {
            switchPhaseToggle.enabled = isOn;
            switchPhaseToggle.gameObject.SetActive(isOn);
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