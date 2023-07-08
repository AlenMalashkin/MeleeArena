using Code.Infrastructure.GameStates;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.MenuWindow
{
	public class MenuWindow : WindowBase
	{
		[SerializeField] private Button play;

		private IGameStateMachine _gameStateMachine;
		
		public void Construct(IGameStateMachine gameStateMachine)
		{
			_gameStateMachine = gameStateMachine;
		}
		
		protected override void OnAwake()
		{
			play.onClick.AddListener(() => _gameStateMachine.Enter<LoadProgressState>());
		}
	}
}