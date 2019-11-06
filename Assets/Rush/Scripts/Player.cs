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
        [SerializeField] private GameObject preview;

        private int inventoryIndex = 0;
        public GameObject Levelinventory;
        private List<ElementInventory> inventory;
        private ElementInventory elementInHand;
        private GameObject objectInHand;


        private void Start() {
            inventory = Levelinventory.GetComponent<Inventory>().list;
            Instantiate(preview);
        }


        private void OnScroll() {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0) {

                inventoryIndex += (int)scroll;
                inventoryIndex = Mathf.Clamp(inventoryIndex, 0, inventory.Count - 1);

                //Destroy(ObjectInHand);
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
                    objectInHand = Instantiate(elementInHand.Tiles[0]);
                    Debug.Log("Object in hand" + objectInHand);
                    Debug.Log("Element 0 in hand" + elementInHand.Tiles[0]);
                    objectInHand.transform.rotation = inventory[inventoryIndex].Direction;
                    objectInHand.layer = 2; //ignore raycast
                }
            }
        }

        private void RaycastToGround() {


            //if (elementInHand.Tiles.Count == 0) {
            //    return;
            //}


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, groundMask)) {
                Transform ground = hit.collider.transform;

                Ray rayAboveGround = new Ray(ground.position, Vector3.up);
                RaycastHit hitAbove;
                bool isFree = !Physics.Raycast(rayAboveGround, out hitAbove, 3);


                OnClick(isFree, hitAbove);

                GetElementInHand();
                //if (ObjectInHand != null) {

                Debug.Log("Transform" + objectInHand.transform);
                    objectInHand.transform.position = ground.position + Vector3.up / 2;
                //}


                if (!isFree) {
                    //ObjectInHand.SetActive(false);
                } else {
                    objectInHand.SetActive(true);

                }
            } else {
                //ObjectInHand.SetActive(false);
            }

        }

        private void PutTileDown() {
            objectInHand.layer = 0;
            if (elementInHand.Tiles.Count > 0) {
                elementInHand.Tiles.RemoveAt(0);
                if (elementInHand.Tiles.Count == 0) {
                    inventoryIndex = inventoryIndex >= inventory.Count - 1 ? inventory.Count - 1 : inventoryIndex + 1;
                    //ObjectInHand = null;
                }
                Debug.Log(inventoryIndex);
                elementInHand = null;
            }
        }

        private void OnClick(bool isFree, RaycastHit above) {
            if (Input.GetMouseButtonUp(0)) {
                Debug.Log(isFree);
                if (isFree) {
                    Debug.Log(objectInHand);
                    PutTileDown();
                } else if (above.collider.GetComponent<DraggableTile>()) {
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