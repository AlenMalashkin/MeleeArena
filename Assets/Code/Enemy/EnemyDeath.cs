using System;
using Code.UI.Elements;
using UnityEngine;

namespace Code.Enemy
{
	public class EnemyDeath : MonoBehaviour
	{
		public event Action EnemyDied;
		
		[SerializeField] private EnemyAnimator animator;
		[SerializeField] private ActorUI actorUI;
		[SerializeField] private HpBar hpBar;
		[SerializeField] private EnemyHealth enemyHealth;
		[SerializeField] private EnemyAttack attack;
		[SerializeField] private EnemyMovement movement;
		[SerializeField] private Collider[] colliders;

		private void OnEnable()
		{
			enemyHealth.HealthChanged += CheckHealth;
		}

		private void OnDisable()
		{
			enemyHealth.HealthChanged += CheckHealth;
		}

		private void CheckHealth(int health)
		{
			if (health <= 0)
				Die();
		}
		
		private void Die()
		{
			animator.PlayDie();
			EnemyDied?.Invoke();
			Destroy(gameObject, 3);
		}

		private void DisableEnemy()
		{
			hpBar.gameObject.SetActive(false);
			attack.enabled = false;
			actorUI.enabled = false;
			movement.enabled = false;
			foreach (var collider in colliders)
				collider.enabled = false;
		}
	}
}