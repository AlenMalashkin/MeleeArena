using System;
using Code.Logic;
using UnityEngine;

namespace Code.Enemy
{
	public class EnemyHealth : MonoBehaviour, IHealth
	{
		public event Action<int> HealthChanged;

		private int _maxEnemyHealth;
		private int _currentEnemyHealth;

		public void Construct(int maxHealth, int currentHealth)
		{
			MaxHealth = maxHealth;
			CurrentHealth = currentHealth;
		}
		
		public int MaxHealth
		{
			get => _maxEnemyHealth; 
			set => _maxEnemyHealth = value;
		}

		public int CurrentHealth
		{
			get => _currentEnemyHealth = _currentEnemyHealth < 0 ? 0 : _currentEnemyHealth;
			set
			{
				_currentEnemyHealth = value;
				HealthChanged?.Invoke(value);
			}
		}
		public void TakeDamage(int damage)
		{
			if (_currentEnemyHealth <= 0)
				return;

			CurrentHealth -= damage;
		}
	}
}