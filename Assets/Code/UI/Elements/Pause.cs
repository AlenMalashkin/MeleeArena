using Code.Services;
using Code.UI.Services.WindowService;
using Code.UI.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Elements
{
	public class Pause : MonoBehaviour
	{
		[SerializeField] private Button pauseButton;

		private IWindowService _windowService;
		
		private void Awake()
		{
			_windowService = ServiceLocator.Container.Resolve<IWindowService>();
		}

		private void OnEnable()
		{
			pauseButton.onClick.AddListener(OpenPauseWindow);
		}

		private void OnDisable()
		{
			pauseButton.onClick.RemoveListener(OpenPauseWindow);
		}

		private void OpenPauseWindow()
			=> _windowService.Open(WindowType.PauseWindow);
	}
}