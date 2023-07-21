using Code.Services;
using Code.Services.Bank;
using UnityEngine;

namespace Code.Enemy
{
	public class DropLoot : MonoBehaviour
	{
		[SerializeField] private Loot lootPrefab;
		[SerializeField] private EnemyDeath enemyDeath;
		[SerializeField] private int reward;

		private IBank _bank;
		
		private void Awake()
		{
			_bank = ServiceLocator.Container.Resolve<IBank>();
		}

		private void OnEnable()
		{
			enemyDeath.EnemyDied += OnEnemyDied;
		}

		private void OnDisable()
		{
			enemyDeath.EnemyDied -= OnEnemyDied;
		}

		private void OnEnemyDied()
		{
			Loot loot = Instantiate(lootPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
			loot.Construct(_bank, reward);
		}
	}
}