///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 27/10/2019 15:47
///-----------------------------------------------------------------

using Com.IsartDigital.Common;
using UnityEngine;

namespace Com.IsartDigital.Rush.Tiles {
    public class Target : ATile {

        [SerializeField] private ColorChanger.EColor color;
        [SerializeField] private Transform render;
        [SerializeField] private ParticleSystem particleSystemBurst;

        private MaterialPropertyBlock block;
        private ColorChanger colorChanger;
        private int colorIndex;

        protected override void Start() {
            base.Start();
            ChangeColor();
        }

        private void OnValidate() {
            ChangeColor();
        }

        private void ChangeColor() {
            colorChanger = GetComponent<ColorChanger>();
            block = colorChanger.ChangeColor(color);

            for (int i = render.childCount - 1; i >= 0; i--) {
                render.GetChild(i).GetComponent<Renderer>().SetPropertyBlock(block);
            }
            colorIndex = (int)color;
        }

        public override void SetCubeAction(Cube cube) {
            if (cube.colorIndex == colorIndex) {
                particleSystemBurst.Play();

                ParticleSystem.MainModule main = particleSystemBurst.main;
                main.startColor = block.GetColor("_Color");
                cube.SetModeOnTarget();

                GetComponent<AudioSource>().Play();
            }
        }
    }
}