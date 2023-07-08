using Code.Logic;
using UnityEngine;

namespace Code.UI.Elements
{
	public class ActorUI : MonoBehaviour
	{
		[SerializeField] private HpBar hpBar;

		private IHealth _health;
		
		public void Construct(IHealth health)
		{
			_health = health;
			_health.HealthChanged += UpdateHealthBar;
		}

		private void OnDisable()
		{
			_health.HealthChanged += UpdateHealthBar;
		}

		private void UpdateHealthBar(int health)
		{
			hpBar.SetValue(health, _health.MaxHealth);
		}
	}
}