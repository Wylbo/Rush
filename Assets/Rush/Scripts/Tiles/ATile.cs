///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 21/10/2019 15:25
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.IsartDigital.Rush.Tiles {
	public abstract class ATile : MonoBehaviour {

        [SerializeField] protected LayerMask cubeMask;

        protected Vector3 raycastOffset = new Vector3(0, 0.5f, 0);
        protected virtual void Start() {
            TimeManager.Instance.OnTick += Tick;
            //TimeManager.Instance.OnStartTick += Tick;
        }

        protected virtual void Tick() {
            CubeAbove();
        }

  //      protected virtual void Update () {
  //          CubeAbove();
		//}

        protected virtual void CubeAbove() {
            Ray ray = new Ray(transform.position - raycastOffset, Vector3.up);
            RaycastHit cube;

            Debug.DrawRay(transform.position, Vector3.up, Color.red);

            bool isCubeAbove = Physics.Raycast(ray,out cube, 1, cubeMask);

            if (isCubeAbove) {
                SetCubeAction(cube.collider.GetComponent<Cube>());
                Debug.Log("action");
            }

        }

        protected virtual void SetCubeAction(Cube cube) { }
	}
}