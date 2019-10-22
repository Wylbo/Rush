///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 22/10/2019 17:19
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.IsartDigital.Rush.Tiles {
	public class StopTile : ATile {

        protected override void SetCubeAction(Cube cube) {
            base.SetCubeAction(cube);
            Debug.Log("Stop");
            cube.SetModeWait();
        }
    }
}