///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 08/11/2019 10:24
///-----------------------------------------------------------------

using Com.IsartDigital.Rush.Tiles;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.IsartDigital.Rush.Hud {
    public class Hud : MonoBehaviour {

        [SerializeField] private RectTransform tileButtonContainer;
        [SerializeField] private GameObject level;
        [SerializeField] private GameObject uiButton;

        private List<ElementInventory> levelInventory;

        private static Hud instance;
        public static Hud Instance { get { return instance; } }
        private void Awake() {
            if (instance) {
                Destroy(gameObject);
                return;
            }

            instance = this;
        }

        public void Init() {
            levelInventory = level.GetComponent<Inventory>().list;
            GameObject uiTile;
            GameObject button;
            for (int i = 0; i < levelInventory.Count; i++) {
                button = Instantiate(uiButton,tileButtonContainer,false);
                button.transform.localScale = Vector3.one;
                button.GetComponentInChildren<Text>().text = levelInventory[i].Number.ToString();
                

                uiTile = Instantiate(levelInventory[i].UIPrefab,button.transform,false); 

                uiTile.transform.localPosition = Vector3.zero;
                uiTile.transform.localScale *= 100;
                uiTile.transform.rotation = levelInventory[i].Direction;
                uiTile.transform.rotation = Camera.main.transform.rotation * uiTile.transform.rotation;
                uiTile.transform.rotation = Quaternion.AngleAxis(-90, Vector3.right) * uiTile.transform.rotation;
            }
        }

        private void Start() {

        }

        private void Update() {

        }

        private void OnDestroy() {
            if (this == instance) instance = null;
        }
    }
}