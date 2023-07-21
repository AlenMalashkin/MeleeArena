using Code.Data;
using Code.Services.GameOverReporterService;
using Code.Services.GameplayObjectsService;
using Code.Services.KillCountService;
using Code.Services.WaveService;
using UnityEngine;

namespace Code.Infrastructure.GameStates
{
	public class GameState : IState
	{
		private IGameStateMachine _gameStateMachine;
		private IKillCountService _killCountService;
		private IWaveService _waveService;

		public GameState(IGameStateMachine gameStateMachine, IKillCountService killCountService, IWaveService waveService)
		{
			_gameStateMachine = gameStateMachine;
			_killCountService = killCountService;
			_waveService = waveService;
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
			if (killCount >= WaveSettingsByWaveType.GetWaveSettingsByWaveType(_waveService.Wave).KillCount)
				_gameStateMachine.Enter<GameOverState, GameResults>(GameResults.Win);
		}
	}
}