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
                Destroy(cube.gameObject);
            }
        }
    }
}