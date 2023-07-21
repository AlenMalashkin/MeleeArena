using System;
using Code.Services.GameOverReporterService;
using Code.Services.GameplayObjectsService;
using Code.UI.Services.WindowService;
using Code.UI.Windows;

namespace Code.Infrastructure.GameStates
{
	public class GameOverState : IPayloadedState<GameResults>
	{
		private IWindowService _windowService;
		private IGameResultReporterService _gameResultReporterService;

		public GameOverState(IWindowService windowService, IGameResultReporterService gameResultReporterService)
		{
			_windowService = windowService;
			_gameResultReporterService = gameResultReporterService;
		}
		
		public void Enter(GameResults results)
		{
			_gameResultReporterService.ReportResults(results);

			switch (results)
			{
				case GameResults.Win:
					_windowService.Open(WindowType.WinWindow);
					break;
				case GameResults.Lose:
					_windowService.Open(WindowType.LoseWindow);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(results), results, null);
			}
		}

		public void Exit()
		{
		}
	}
}