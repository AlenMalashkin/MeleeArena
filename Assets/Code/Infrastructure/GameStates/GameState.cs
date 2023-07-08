using Code.Player;
using UnityEngine;

namespace Code.Infrastructure.GameStates
{
	public class GameState : IPayloadedState<GameObject>
	{
		private IGameStateMachine _gameStateMachine;
		private GameObject _player;
		private PlayerDeath _playerDeath;
		
		public GameState(IGameStateMachine gameStateMachine)
		{
			_gameStateMachine = gameStateMachine;
		}

		public void Enter(GameObject payload)
		{
			_player = payload;
			_playerDeath = _player.GetComponent<PlayerDeath>();
			_playerDeath.PlayerDied += OnPlayerDied;
		}

		public void Exit()
		{
			_playerDeath.PlayerDied -= OnPlayerDied;
		}

		private void OnPlayerDied()
		{
			_gameStateMachine.Enter<GameOverState>();
		}
	}
}