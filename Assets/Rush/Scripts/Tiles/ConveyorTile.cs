///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 23/10/2019 16:06
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.IsartDigital.Rush.Tiles {
    public class ConveyorTile : ADraggableTile {

        public override void SetCubeAction(Cube cube) {
            base.SetCubeAction(cube);
            cube.SetModeConvoyed(transform.forward);

        }
    }
}