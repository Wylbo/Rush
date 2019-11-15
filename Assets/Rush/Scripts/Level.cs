///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 05/11/2019 10:55
///-----------------------------------------------------------------

using Com.IsartDigital.Rush.Tiles;
using System.Collections.Generic;
using UnityEngine;

namespace Com.IsartDigital.Rush {
	public class Level : MonoBehaviour {

        [SerializeField] public List<ElementInventory> list;


        public void Init() {
            for (int i = list.Count - 1; i >= 0; i--) {
                list[i].Init();
            }
        }

    }
}