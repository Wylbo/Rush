///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 23/11/2019 23:39
///-----------------------------------------------------------------

using Com.IsartDigital.Rush.Manager;
using UnityEngine;

namespace Com.IsartDigital.Rush.Ui {
	public class TitleCard : MonoBehaviour {

        [SerializeField] private GameObject levelSelector;
		private void Start () {
			
		}
		
		private void Update () {
            if (Input.anyKey) {
                HudManager.Instance.AddScreen(levelSelector);
                HudManager.Instance.RemoveScreen(gameObject);
            }
		}
	}
}