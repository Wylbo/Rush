///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 21/10/2019 15:34
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.IsartDigital.Rush.Tiles {
    public class ArrowTile : ADraggableTile {
        public override void SetCubeAction(Cube cube) {
            base.SetCubeAction(cube);
            cube.SetDirection(transform.forward);   
        }
    }
}