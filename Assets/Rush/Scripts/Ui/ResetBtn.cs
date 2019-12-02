///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 17/11/2019 14:38
///-----------------------------------------------------------------

using Com.IsartDigital.Rush.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Com.IsartDigital.Rush.Ui {
	public class ResetBtn : MonoBehaviour {

        private Button button;
        [SerializeField] AudioSource click;
		private void Start () {
            button = GetComponent<Button>();
            button.onClick.AddListener(() => { LevelManager.Instance.RestartLevel(); click.Play(); });
		}
		
	}
}