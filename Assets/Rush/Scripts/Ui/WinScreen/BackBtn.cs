///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 14/11/2019 21:47
///-----------------------------------------------------------------

using Com.IsartDigital.Rush.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Com.IsartDigital.Rush.Ui.WinScreen {
	public class BackBtn : MonoBehaviour {

        [SerializeField] GameObject levelSelector;

        private Button button;

		private void Start () {
            button = GetComponent<Button>();
            button.onClick.AddListener(() => { HudManager.Instance.AddScreen(levelSelector);
                HudManager.Instance.RemoveScreen(transform.parent.gameObject);
                LevelManager.Instance.UnloadLevels();
                GameManager.Instance.OnBackToLevelSelector();
            });
        }
	}
}