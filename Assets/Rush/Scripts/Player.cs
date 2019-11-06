///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 05/11/2019 11:33
///-----------------------------------------------------------------

using Com.IsartDigital.Rush.Tiles;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Com.IsartDigital.Rush {
    public class Player : MonoBehaviour {

        [SerializeField] private LayerMask groundMask;

        private int inventoryIndex = 0;
        public GameObject Levelinventory;
        private List<ElementInventory> inventory;
        private ElementInventory elementInHand;
        private GameObject ObjectInHand;


        private void Start() {
            inventory = Levelinventory.GetComponent<Inventory>().list;
        }


        private void OnScroll() {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0) {

                inventoryIndex += (int)scroll;
                inventoryIndex = Mathf.Clamp(inventoryIndex, 0, inventory.Count - 1);

                Destroy(ObjectInHand);
                elementInHand = null;
                Debug.Log(inventoryIndex);
            }
        }

        private void Update() {
            OnScroll();
            RaycastToGround();

            if (Input.GetKeyDown(KeyCode.Space)) {
                GameManager.Instance.SwitchMode();
            }
        }

        private void GetElementInHand() {
            if (elementInHand == null) {
                elementInHand = inventory[inventoryIndex];
                if (elementInHand.Tiles.Count > 0) {
                    ObjectInHand = Instantiate(elementInHand.Tiles[0]);
                    ObjectInHand.transform.rotation = inventory[inventoryIndex].Direction;
                    ObjectInHand.layer = 2; //ignore raycast
                }
            }
        }

        private void RaycastToGround() {
            //if (inventory.Count == 0) {
            //    return;
            //}

            GetElementInHand();

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, groundMask)) {
                Transform ground = hit.collider.transform;

                Ray rayAboveGround = new Ray(ground.position, Vector3.up);
                RaycastHit hitAbove;
                bool isFree = !Physics.Raycast(rayAboveGround, out hitAbove, 3);



                ObjectInHand.transform.position = ground.position + Vector3.up / 2;

                OnClick(isFree, hitAbove);

                if (!isFree) {
                    ObjectInHand.SetActive(false);
                } else {
                    ObjectInHand.SetActive(true);

                }
            } else {
                ObjectInHand.SetActive(false);
            }

        }

        private void PutTileDown() {
            ObjectInHand.layer = 0;
            elementInHand.Tiles.RemoveAt(0);
            if (elementInHand.Tiles.Count == 0) {
                inventoryIndex = inventoryIndex >= inventory.Count - 1 ? inventory.Count - 1 : inventoryIndex + 1;

            }
            Debug.Log(inventoryIndex);
            elementInHand = null;
        }

        private void OnClick(bool isFree, RaycastHit above) {
            if (Input.GetMouseButtonUp(0)) {
                if (isFree) {
                    PutTileDown();
                } else if (above.collider.GetComponent<DraggableTile>() != null) {
                    RemoveTile(above.collider.gameObject);
                }
            }
        }

        private void RemoveTile(GameObject above) {
            for (int i = 0; i < inventory.Count; i++) {
                if (inventory[i].CompareType(above)) {
                    inventory[i].AddOneToList();
                    Destroy(above);
                }
            }
        }
    }
}