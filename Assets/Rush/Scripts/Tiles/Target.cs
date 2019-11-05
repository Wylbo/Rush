///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 27/10/2019 15:47
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.IsartDigital.Rush.Tiles {
    public class Target : ATile {

        [SerializeField] Color color;
        [SerializeField] MaterialPropertyBlock block;

        protected override void Start() {
            base.Start();
            ChangeColor();
        }

        private void OnValidate() {
            ChangeColor();
        }

        private void ChangeColor() {
            block = new MaterialPropertyBlock();
            block.SetColor("_Color", color);
            block.SetColor("_EmissionColor", color);

            for (int i = 0; i < transform.childCount; i++) {
                transform.GetChild(i).GetComponent<Renderer>().SetPropertyBlock(block);
            }
        }

        public override void SetCubeAction(Cube cube) {
            if (cube.GetComponent<Renderer>().material.color == color) {
                Destroy(cube.gameObject);
            }
        }
    }
}