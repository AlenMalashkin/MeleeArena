using System;
using Code.Logic;
using UnityEngine;

namespace Code.Player
{
    public class PlayerHealth : MonoBehaviour, IHealth
    {
        public event Action<int> HealthChanged;

        [SerializeField] private PlayerAnimator animator;
        
        private int _maxHealth;
        private int _currentHealth;

        public int MaxHealth
        {
            get => _maxHealth;
            set => _maxHealth = value;
        }

        public int CurrentHealth
        {
            get => _currentHealth <= 0 ? 0 : _currentHealth;
            set
            {
                _currentHealth = value;
                HealthChanged?.Invoke(value);
            }
        }

        public void TakeDamage(int damage)
        {
            if (_currentHealth <= 0)
                return;

            animator.PlayHit();
            CurrentHealth -= damage;
        }
    }
}
