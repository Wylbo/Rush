///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 05/11/2019 11:33
///-----------------------------------------------------------------

using Com.IsartDigital.Rush.Tiles;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Com.IsartDigital.Rush {
	public class TilePlacer : MonoBehaviour {

        [SerializeField] private LayerMask groundMask;

        private int inventoryIndex = 0;
        private List<ElementInventory> inventory;

        private void Start () {
            inventory = transform.parent.GetComponent<Inventory>().list;

        }

        private void Update () {
            RaycastToGround();
		}

        private void RaycastToGround() {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray,out hit,100,groundMask)) {
                if (Input.GetMouseButtonUp(0)) {
                    Transform ground = hit.collider.transform;
                    
                    Ray testAboveGround = new Ray(ground.position, Vector3.up);
                    if (!Physics.Raycast(testAboveGround,3)) {
                        PutTileDown(ground.position + Vector3.up / 2);
                    }
                }
            }

        }

        private void PutTileDown(Vector3 position) {
            if (inventory.Count == 0) {
                return;
            }

            GameObject tile = Instantiate(inventory[inventoryIndex].Tiles);

            tile.transform.position = position;
            tile.transform.rotation = inventory[inventoryIndex].Direction;

            inventory.RemoveAt(inventoryIndex);
        }
    }
}