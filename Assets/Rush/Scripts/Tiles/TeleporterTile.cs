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
            TeleporterTile targetScript = target.GetComponent<TeleporterTile>();
            targetScript.color = color;

            for (int i = 0; i < particules.Count; i++) {
                ParticleSystem.MainModule main = particules[i].main;
                //main.startColor = new ParticleSystem.MinMaxGradient(color);
            }

            for (int j = 0; j < targetScript.particules.Count; j++) {
                ParticleSystem.MainModule main = targetScript.particules[j].main;
                //main.startColor = new ParticleSystem.MinMaxGradient(color);
            }
        }


        public override void SetCubeAction(Cube cube) {
            cube.SetModeTeleport(target);


        }
    }
}