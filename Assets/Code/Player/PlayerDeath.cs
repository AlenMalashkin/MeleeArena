using Code.Infrastructure.GameStates;
using Code.Logic;
using Code.Services.GameOverReporterService;
using Code.SFX;
using UnityEngine;

namespace Code.Player
{
	public class PlayerDeath : MonoBehaviour
	{
		[SerializeField] private PlayerAnimator animator;
		[SerializeField] private PlayerHealth playerHealth;
		[SerializeField] private SfxPlayer sfxPlayer;
		[SerializeField] private ParticleSystem deathParticles;
		[SerializeField] private AudioClip deathSound;

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
			deathParticles.Play();
			sfxPlayer.PlaySfx(deathSound);
			_gameStateMachine.Enter<GameOverState, GameResults>(GameResults.Lose);
		}
	}
}