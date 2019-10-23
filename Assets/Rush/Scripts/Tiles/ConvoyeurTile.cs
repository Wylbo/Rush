///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 23/10/2019 16:06
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.IsartDigital.Rush.Tiles {
    public class ConvoyeurTile : ATile {

        protected override void SetCubeAction(Cube cube) {
            base.SetCubeAction(cube);
            cube.SetModeWait(1);
            cube.transform.position = Vector3.Lerp(cube.transform.position, cube.transform.position + transform.forward, TimeManager.Instance.Ratio);
            
        }
    }
}