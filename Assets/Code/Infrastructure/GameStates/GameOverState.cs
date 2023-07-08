using Code.UI.Services.WindowService;
using Code.UI.Windows;
using UnityEngine;

namespace Code.Infrastructure.GameStates
{
	public class GameOverState : IState
	{
		private IWindowService _windowService;
		
		public GameOverState(IWindowService windowService)
		{
			_windowService = windowService;
		}
		
		public void Enter()
		{
			_windowService.Open(WindowType.LoseWindow);
		}

		public void Exit()
		{
			
		}
	}
}