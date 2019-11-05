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
        private GameObject objectInHand;

        private void Start() {
            inventory = Levelinventory.GetComponent<Inventory>().list;

        }

        private void Update() {
            RaycastToGround();

            if (Input.GetKeyDown(KeyCode.Space)) {
                GameManager.Instance.SwitchMode();
            }
        }

        private void RaycastToGround() {
            if (inventory.Count == 0) {
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (objectInHand == null && inventory.Count > 0) {
                objectInHand = Instantiate(inventory[inventoryIndex].Tiles);
                objectInHand.transform.rotation = inventory[inventoryIndex].Direction;
                objectInHand.tag = "IsInHand";
                objectInHand.layer = 2; //ignore raycast
                objectInHand.SetActive(false);
            }

            if (Physics.Raycast(ray, out hit, 100, groundMask)) {
                Transform ground = hit.collider.transform;

                Ray rayAboveGround = new Ray(ground.position, Vector3.up);
                RaycastHit hitAbove;
                bool isFree = !Physics.Raycast(rayAboveGround, out hitAbove, 3);

                if (isFree) {
                    objectInHand.SetActive(true);

                    objectInHand.transform.position = ground.position + Vector3.up / 2;

                    if (Input.GetMouseButtonUp(0)) {
                        PutTileDown();
                    }


                } else {
                    objectInHand.SetActive(false);
                }
            } else {
                objectInHand.SetActive(false);
            }

        }

        private void PutTileDown() {
            objectInHand.layer = 0;
            inventory.RemoveAt(inventoryIndex);
            objectInHand = null;
        }
    }
}