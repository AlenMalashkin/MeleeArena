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
		private LoadingCurtain.LoadingCurtain _loadingCurtain;

		public void Construct(IGameStateMachine gameStateMachine, LoadingCurtain.LoadingCurtain loadingCurtain)
		{
			_gameStateMachine = gameStateMachine;
			_loadingCurtain = loadingCurtain;
		}

		protected override void OnAwake()
		{
			restartButton.onClick.AddListener(Restart);
			backButton.onClick.AddListener(Back);
		}

		private void Restart()
		{
			_gameStateMachine.Enter<LoadProgressState>();
		}

		private void Back()
		{
			_gameStateMachine.Enter<MenuState>();
		}
	}
}