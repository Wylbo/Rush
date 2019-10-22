///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 21/10/2019 15:25
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.IsartDigital.Rush.Tiles {
	public abstract class ATile : MonoBehaviour {

        [SerializeField] protected LayerMask cubeMask;
		
		protected virtual void Update () {
            CubeAbove();
		}

        protected virtual void CubeAbove() {
            Ray ray = new Ray(transform.position + Vector3.down, Vector3.up);
            RaycastHit cube;

            Debug.DrawRay(transform.position + Vector3.down, Vector3.up * 100, Color.red);

            bool isCubeAbove = Physics.Raycast(ray,out cube, 100, cubeMask);

            if (isCubeAbove) {
                SetCubeAction(cube.collider.GetComponent<Cube>());
            }

        }

        protected virtual void SetCubeAction(Cube cube) { }
	}
}