///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 08/11/2019 10:24
///-----------------------------------------------------------------

using Com.IsartDigital.Rush.Tiles;
using System.Collections.Generic;
using UnityEngine;

namespace Com.IsartDigital.Rush.Hud {
    public class Hud : MonoBehaviour {

        [SerializeField] private RectTransform tileButtonContainer;
        [SerializeField] private GameObject level;

        private List<ElementInventory> levelInventory;

        private void Init() {
            levelInventory = level.GetComponent<Inventory>().list;
            GameObject tile;

            for (int i = 0; i < levelInventory.Count; i++) {
                tile = Instantiate(levelInventory[i].Tile);

                tile.transform.SetParent(tileButtonContainer);
                tile.layer = 5;
            }
        }



        private void Start () {
            Init();
		}
		
		private void Update () {
			
		}
	}
}