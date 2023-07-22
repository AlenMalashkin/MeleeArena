using Code.Services.Bank;
using UnityEngine;

namespace Code.Enemy
{
	public class Loot : MonoBehaviour
	{
		[SerializeField] private TriggerObserver triggerObserver;
		
		private IBank _bank;
		private int _reward;

		private void OnEnable()
		{
			triggerObserver.TriggerEntered += Collect;
		}

		private void OnDisable()
		{
			triggerObserver.TriggerEntered -= Collect;
		}

		public void Construct(IBank bank, int reward)
		{
			_bank = bank;
			_reward = reward;
		}

		private void Collect(Collider other)
		{
			if (!other.TryGetComponent(out Player.Player player))
				return;
			
			_bank.GetMoney(_reward);
			Destroy(gameObject);
		}
	}
}