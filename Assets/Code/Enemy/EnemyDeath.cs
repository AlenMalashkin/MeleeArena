using System;
using UnityEngine;

namespace Code.Enemy
{
	public class EnemyDeath : MonoBehaviour
	{
		[SerializeField] private EnemyAnimator animator;
		[SerializeField] private EnemyHealth enemyHealth;
		[SerializeField] private EnemyAttack attack;
		[SerializeField] private EnemyMovement movement;

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
			attack.enabled = false;
			movement.enabled = false;
		}
	}
}