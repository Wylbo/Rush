///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 22/10/2019 17:19
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.IsartDigital.Rush.Tiles {
	public class StopTile : ATile {

        public override void SetCubeAction(Cube cube) {
            base.SetCubeAction(cube);

            cube.SetModeWait(2);
        }
    }
}