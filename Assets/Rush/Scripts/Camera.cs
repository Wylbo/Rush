///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 21/10/2019 17:06
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.IsartDigital.Rush {
	public class Camera : MonoBehaviour {
		private static Camera instance;

        [SerializeField] private Vector3 pivot;
        [SerializeField] private string horizontalAxis;

		public static Camera Instance { get { return instance; } }
		
		private void Awake(){
			if (instance){
				Destroy(gameObject);
				return;
			}
			
			instance = this;
		}
		
		private void Start () {
			
		}
		
		private void Update () {
			Input.GetAxis()
		}
		
		private void OnDestroy(){
			if (this == instance) instance = null;
		}
	}
}