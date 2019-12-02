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

        private bool hasClicked = false;
		private void Update () {
            if (Input.anyKey && !hasClicked) {
                hasClicked = true;
                GetComponent<Animator>().SetTrigger("Start");
                GetComponent<AudioSource>().Play();
            }
		}

        private void Launch() {
            HudManager.Instance.AddScreen(levelSelector);
            HudManager.Instance.RemoveScreen(gameObject);
        }
	}
}