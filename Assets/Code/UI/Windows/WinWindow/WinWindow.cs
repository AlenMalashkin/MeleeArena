using Code.Infrastructure.GameStates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.WinWindow
{
	public class WinWindow : WindowBase
	{
		[SerializeField] private Button nextWaveButton;
		[SerializeField] private Button backButton;

		private IGameStateMachine _gameStateMachine;

		public void Construct(IGameStateMachine gameStateMachine)
		{
			_gameStateMachine = gameStateMachine;
		}

		protected override void OnAwake()
		{
			nextWaveButton.onClick.AddListener(NextWave);
			backButton.onClick.AddListener(Back);
		}

		private void NextWave()
		{
			_gameStateMachine.Enter<LoadLevelState, string>("Main");
		}

		private void Back()
		{
			_gameStateMachine.Enter<MenuState>();
		}
	}
}