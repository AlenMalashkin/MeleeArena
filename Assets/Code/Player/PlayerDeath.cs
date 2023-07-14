using Code.Infrastructure.GameStates;
using Code.Logic;
using Code.Services.GameOverReporterService;
using UnityEngine;

namespace Code.Player
{
	public class PlayerDeath : MonoBehaviour
	{
		[SerializeField] private PlayerAnimator animator;
		[SerializeField] private PlayerHealth playerHealth;

		private IGameStateMachine _gameStateMachine;

		public void Construct(IGameStateMachine gameStateMachine)
		{
			_gameStateMachine = gameStateMachine;
		}
		
		private void OnEnable()
		{
			playerHealth.HealthChanged += OnHealthChanged;
		}

		private void OnDisable()
		{
			playerHealth.HealthChanged -= OnHealthChanged;
		}

		private void OnHealthChanged(int health)
		{
			if (health <= 0)
				Die();
		}

		private void Die()
		{
			animator.PlayDeath();
			_gameStateMachine.Enter<GameOverState, GameResults>(GameResults.Lose);
		}
	}
}