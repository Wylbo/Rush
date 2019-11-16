///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 06/11/2019 15:03
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.IsartDigital.Rush.Tiles {
	public abstract class ADraggableTile : ATile {

        [SerializeField] private GameObject uiTile;

        public GameObject UiTile { get { return uiTile; } private set { uiTile = value; } }
	
		//private void Start () {
			
		//}
		
		//private void Update () {
			
		//}
	}
}