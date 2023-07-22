using Code.Infrastructure.GameStates;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.PauseWindow
{
	public class PauseWindow : WindowBase
	{
		[SerializeField] private Button resumeButton;
		[SerializeField] private Button backButton;

		private IGameStateMachine _gameStateMachine;
		
		public void Construct(IGameStateMachine gameStateMachine)
		{
			_gameStateMachine = gameStateMachine;
		}
		
		protected override void OnAwake()
		{
			Time.timeScale = 0;
		}

		private void OnEnable()
		{
			resumeButton.onClick.AddListener(Resume);
			backButton.onClick.AddListener(Back);
		}

		private void OnDisable()
		{
			resumeButton.onClick.RemoveListener(Resume);
			backButton.onClick.RemoveListener(Back);
		}

		private void Back()
		{
			Time.timeScale = 1;
			_gameStateMachine.Enter<MenuState>();
		}

		private void Resume()
		{
			Time.timeScale = 1;
			Destroy(gameObject);
		}
	}
}