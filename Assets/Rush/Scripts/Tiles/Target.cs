///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 27/10/2019 15:47
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.IsartDigital.Rush.Tiles {
    public class Target : ATile {

        [SerializeField] Color color;

        private void OnValidate() {
            for (int i = 0; i < transform.childCount; i++) {
                transform.GetChild(i).GetComponent<Renderer>().sharedMaterial.SetColor("_Color", color);

            }
        }

        public override void SetCubeAction(Cube cube) {
            if (cube.GetComponent<Renderer>().material.GetColor("_Color") == color) {
               Destroy(cube.gameObject);
            }
        }
    }
}