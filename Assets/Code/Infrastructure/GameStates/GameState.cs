using Code.Services.GameOverReporterService;
using Code.Services.GameplayObjectsService;
using Code.Services.KillCountService;
using UnityEngine;

namespace Code.Infrastructure.GameStates
{
	public class GameState : IState
	{
		private IGameStateMachine _gameStateMachine;
		private IKillCountService _killCountService;

		public GameState(IGameStateMachine gameStateMachine, IKillCountService killCountService)
		{
			_gameStateMachine = gameStateMachine;
			_killCountService = killCountService;
		}

		public void Enter()
		{
			_killCountService.Reset();
			_killCountService.KillCountChanged += OnKillCountChanged;
		}

		public void Exit()
		{
			_killCountService.KillCountChanged -= OnKillCountChanged;
		}

		private void OnKillCountChanged(int killCount)
		{
			if (killCount >= 3)
				_gameStateMachine.Enter<GameOverState, GameResults>(GameResults.Win);
		}
	}
}