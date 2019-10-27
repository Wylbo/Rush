///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 24/10/2019 20:00
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.IsartDigital.Rush.Tiles {
    public class TeleporterTile : ATile {

        [SerializeField] private Transform target;


        protected override void Tick() {
            base.Tick();
        }

        public override void SetCubeAction(Cube cube) {
            cube.SetModeTeleport(target);


        }
    }
}