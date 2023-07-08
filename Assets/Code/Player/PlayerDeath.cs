using System;
using UnityEngine;

namespace Code.Player
{
	public class PlayerDeath : MonoBehaviour
	{
		public event Action PlayerDied;
		
		[SerializeField] private PlayerHealth playerHealth;
		[SerializeField] private PlayerMovement playerMovement;
		[SerializeField] private PlayerAttack playerAttack;

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
			playerMovement.enabled = false;
			playerAttack.enabled = false;
			PlayerDied?.Invoke();
		}
	}
}