///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 21/10/2019 17:06
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.IsartDigital.Rush {
    public class Camera : MonoBehaviour {
        private static Camera instance;

        [SerializeField] private Vector3 pivot;
        [SerializeField] private float speed;
        [SerializeField] private string horizontalAxis;
        [SerializeField] private string verticalAxis;

        private float distanceFromPivot;

        public static Camera Instance { get { return instance; } }
		
		private void Awake(){
			if (instance){
				Destroy(gameObject);
				return;
			}
			
			instance = this;
		}
		
		private void Start () {
            distanceFromPivot = Vector3.Distance(pivot, transform.position);
            transform.LookAt(pivot);
		}
		
		private void Update () {
            //float vertAngle;
            //float horiAngle;

            transform.LookAt(pivot);

            transform.Translate(new Vector3(Input.GetAxis(horizontalAxis),  Input.GetAxis(verticalAxis)) * Time.deltaTime * speed);
		}
		
		private void OnDestroy(){
			if (this == instance) instance = null;
		}
	}
}