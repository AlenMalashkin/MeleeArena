using Code.Infrastructure.GameStates;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.LoseWindow
{
	public class LoseWindow : WindowBase
	{
		[SerializeField] private Button restartButton;
		[SerializeField] private Button backButton;

		private IGameStateMachine _gameStateMachine;

		public void Construct(IGameStateMachine gameStateMachine)
		{
			_gameStateMachine = gameStateMachine;
		}

		protected override void OnAwake()
		{
			restartButton.onClick.AddListener(Restart);
			backButton.onClick.AddListener(Back);
		}

		private void Restart()
		{
			_gameStateMachine.Enter<LoadLevelState, string>("Main");
		}

		private void Back()
		{
			_gameStateMachine.Enter<MenuState>();
		}
	}
}