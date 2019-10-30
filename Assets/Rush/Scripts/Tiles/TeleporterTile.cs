///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 24/10/2019 20:00
///-----------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace Com.IsartDigital.Rush.Tiles {
    public class TeleporterTile : ATile {

        [SerializeField] private Transform target;
        [SerializeField] private Color color;
        [SerializeField] private List<ParticleSystem> particules;

        private void OnValidate() {
            if (target == null) {
                return;
            }
            var targetScript = target.GetComponent<TeleporterTile>();
            targetScript.color = color;

            for (int i = 0; i < particules.Count; i++) {
                var main = particules[i].main;
                main.startColor = color;
            }

            for (int j = 0; j < targetScript.particules.Count; j++) {
                var main = targetScript.particules[j].main;
                main.startColor = color;
            }
        }

        protected override void Tick() {
            base.Tick();
        }

        public override void SetCubeAction(Cube cube) {
            cube.SetModeTeleport(target);


        }
    }
}