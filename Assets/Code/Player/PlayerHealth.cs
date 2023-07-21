using System;
using Code.Logic;
using UnityEngine;

namespace Code.Player
{
    public class PlayerHealth : MonoBehaviour, IHealth
    {
        public event Action<int> HealthChanged;

        [SerializeField] private PlayerAnimator animator;
        
        [SerializeField] private int maxHealth;
        [SerializeField] private int currentHealth;

        public int MaxHealth
        {
            get => maxHealth;
            set => maxHealth = value;
        }

        public int CurrentHealth
        {
            get => currentHealth <= 0 ? 0 : currentHealth;
            set
            {
                currentHealth = value;
                HealthChanged?.Invoke(value);
            }
        }

        public void TakeDamage(int damage)
        {
            if (currentHealth <= 0)
                return;

            animator.PlayHit();
            CurrentHealth -= damage;
        }
    }
}
