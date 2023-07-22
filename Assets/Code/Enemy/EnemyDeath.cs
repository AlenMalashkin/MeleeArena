using System;
using Code.SFX;
using Code.UI.Elements;
using UnityEngine;
using UnityEngine.AI;
using AnimatorState = Code.Logic.AnimatorState;

namespace Code.Enemy
{
	public class EnemyDeath : MonoBehaviour
	{
		public event Action EnemyDied;

		[SerializeField] private EnemyAnimator animator;
		[SerializeField] private ParticleSystem deathParticles;
		[SerializeField] private SfxPlayer sfxPlayer;
		[SerializeField] private AudioClip deathSound;
		[SerializeField] private ActorUI actorUI;
		[SerializeField] private HpBar hpBar;
		[SerializeField] private EnemyHealth enemyHealth;
		[SerializeField] private EnemyAttack attack;
		[SerializeField] private NavMeshAgent agent;
		[SerializeField] private EnemyMovement movement;
		[SerializeField] private Collider[] colliders;

		private void OnEnable()
		{
			enemyHealth.HealthChanged += CheckHealth;
			animator.StateEntered += DisableEnemy;
		}

		private void OnDisable()
		{
			enemyHealth.HealthChanged -= CheckHealth;
			animator.StateEntered -= DisableEnemy;
		}

		private void CheckHealth(int health)
		{
			if (health <= 0)
				Die();
		}
		
		private void Die()
		{
			animator.PlayDie();
			deathParticles.gameObject.SetActive(true);
			deathParticles.Play();
			sfxPlayer.PlaySfx(deathSound);
			EnemyDied?.Invoke();
			Destroy(gameObject, 3);
		}

		private void DisableEnemy(AnimatorState state)
		{
			if (state == AnimatorState.Die)
			{
				hpBar.gameObject.SetActive(false);
				attack.enabled = false;
				actorUI.enabled = false;
				movement.enabled = false;
				agent.isStopped = true;
				foreach (var collider in colliders)
					collider.enabled = false;
			}
		}
	}
}