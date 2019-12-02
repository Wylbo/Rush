///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 02/12/2019 17:38
///-----------------------------------------------------------------

using Com.IsartDigital.Rush.Manager;
using UnityEngine;

namespace Com.IsartDigital.Rush.Ui {
	public class GameOverScreen : MonoBehaviour {

        [SerializeField] GameObject hudinGame;
		private void Start () {
			
		}
		
		private void Update () {
            if (Input.anyKey) {
                GetComponent<Animator>().SetTrigger("Click");
            }
		}

        private void _SuperFunction() {
            HudManager.Instance.RemoveScreen(gameObject);
            HudManager.Instance.AddScreen(hudinGame);
            GameManager.Instance.SwitchToReflexionPhase();
            Hud.Instance.gameObject.GetComponent<Animator>().SetTrigger("Appear");

        }
    }
}