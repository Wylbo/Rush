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
            Ray ray = new Ray(transform.position - new Vector3(0, 1), Vector3.up);
            RaycastHit cube;

            Debug.DrawRay(transform.position, Vector3.up, Color.red);

            bool isCubeAbove = Physics.Raycast(ray,out cube, 100, cubeMask);

            Debug.Log(isCubeAbove);

            if (isCubeAbove) {
                Debug.Log("bonjour");
                SetCubeAction(cube.collider.GetComponent<Cube>());
            }

        }

        protected virtual void SetCubeAction(Cube cube) { }
	}
}