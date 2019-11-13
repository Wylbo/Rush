///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 05/11/2019 11:33
///-----------------------------------------------------------------

using Com.IsartDigital.Rush.Manager;
using Com.IsartDigital.Rush.Tiles;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Com.IsartDigital.Rush {
    public class Player : MonoBehaviour {

        static public Player Instance { get; private set; }

        [SerializeField] private LayerMask groundMask;
        [SerializeField] private GameObject previewPrefab;

        private GameObject preview;
        private int inventoryIndex = 0;
        public GameObject Levelinventory;
        private List<ElementInventory> inventory;
        private ElementInventory elementInHand;
        private GameObject objectInHand;


        private void Awake() {
            if (Instance) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start() {
            inventory = Levelinventory.GetComponent<Inventory>().list;
            preview = Instantiate(previewPrefab);
        }


        private void OnScroll() {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0) {

                inventoryIndex += (int)scroll;
                inventoryIndex = Mathf.Clamp(inventoryIndex, 0, inventory.Count - 1);

                //Destroy(ObjectInHand);
                //elementInHand = null;
            }
        }

        private void Update() {
            OnScroll();
            RaycastToGround();

            if (Input.GetKeyDown(KeyCode.Space)) {
                GameManager.Instance.SwitchMode();
            }
        }

        public void OnHudButtonClick(int index) {
            inventoryIndex = index;
            elementInHand = null;
            ResetPreview();
        }

        private void GetElementInHand() {
            if (elementInHand == null) {
                elementInHand = inventory[inventoryIndex];

                if (elementInHand.Tiles.Count > 0) {

                    preview.transform.rotation = elementInHand.Direction;

                    for (int i = preview.transform.childCount - 1; i >= 0; i--) {
                        if (elementInHand.CompareType(preview.transform.GetChild(i).gameObject)) {
                            preview.transform.GetChild(i).gameObject.SetActive(true);
                            break;
                        }
                    }
                } else {
                    
                }
            }
        }

        private void RaycastToGround() {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, groundMask)) {

                Transform ground = hit.collider.transform;

                Ray rayAboveGround = new Ray(ground.position, Vector3.up);
                RaycastHit hitAbove;
                bool isFree = !Physics.Raycast(rayAboveGround, out hitAbove, 1);


                OnClick(isFree, hitAbove);

                GetElementInHand();
                preview.transform.position = ground.position + Vector3.up / 2;


                if (!isFree) {
                    preview.SetActive(false);
                } else {
                    preview.SetActive(true);

                }
            } else {
                preview.SetActive(false);
            }

        }

        private void ResetPreview() {
            for (int i = preview.transform.childCount - 1; i >= 0; i--) {
                preview.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        private void OnClick(bool isFree, RaycastHit above) {
            if (Input.GetMouseButtonUp(0)) {
                if (isFree) {
                    PutTileDown();
                } else if (above.collider.GetComponent<ADraggableTile>()) {
                    RemoveTile(above.collider.gameObject);
                }
                elementInHand = null;

                ResetPreview();

            }
        }

        private void PutTileDown() {
            if (elementInHand.Tiles.Count > 0) {
                Instantiate(elementInHand.Tiles[0], preview.transform.position, preview.transform.rotation);
                elementInHand.Tiles.RemoveAt(0);

                if (elementInHand.Tiles.Count == 0) {
                    inventoryIndex = inventoryIndex >= inventory.Count - 1 ? inventory.Count - 1 : inventoryIndex + 1;

                }
            }
        }

        private void RemoveTile(GameObject above) {
            for (int i = 0; i < inventory.Count; i++) {
                if (inventory[i].CompareType(above)) {
                    inventory[i].AddOneToList();
                    inventoryIndex = i;
                    Debug.Log(inventoryIndex);
                    Destroy(above);
                }
            }
        }
    }
}