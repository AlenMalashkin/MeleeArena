using System.Threading.Tasks;
using Code.Enemy;
using Code.Infrastructure.Assets;
using Code.Logic;
using Code.Logic.Spawners;
using Code.Player;
using Code.Services.StaticData;
using Code.StaticData;
using Code.UI.Elements;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
	public class GameFactory : IGameFactory
	{
		private IAssetProvider _assetProvider;
		private IStaticDataService _staticDataService;
		
		private GameObject _playerGameObject;

		public GameFactory(IAssetProvider assetProvider, IStaticDataService staticDataService)
		{
			_assetProvider = assetProvider;
			_staticDataService = staticDataService;
		}

		public async Task<GameObject> CreatePlayer(Vector3 at)
		{
			_playerGameObject = await _assetProvider.Instantiate(AssetAddress.PlayerPath);
			PlayerHealth playerHealth = _playerGameObject.GetComponent<PlayerHealth>();
			playerHealth.MaxHealth = 10;
			playerHealth.CurrentHealth = 10;
			return _playerGameObject;
		}

		public async Task<GameObject> CreateHud()
			=> await _assetProvider.Instantiate(AssetAddress.HudPath);

		public async Task<GameObject> CreateEnemy(EnemyType type, Vector3 at)
		{
			EnemyStaticData enemyStaticData = _staticDataService.ForEnemy(type);
			GameObject prefab = await _assetProvider.Load<GameObject>(enemyStaticData.AssetReferenceGameObject);
			GameObject enemy = Object.Instantiate(prefab, at, Quaternion.identity);

			enemy.GetComponent<EnemyMovement>().Construct(enemyStaticData.MoveSpeed, _playerGameObject.transform);
			enemy.GetComponent<EnemyHealth>().Construct(enemyStaticData.Health, enemyStaticData.Health);
			enemy.GetComponent<EnemyAttack>().Construct(_playerGameObject.transform, enemyStaticData.Damage, enemyStaticData.AttackCooldown, enemyStaticData.AttackDistance, enemyStaticData.Cleavage);
			
			return enemy;
		}

		public async Task CreateSpawner(EnemyType type, Vector3 at)
		{
			GameObject prefab = await _assetProvider.Load<GameObject>(AssetAddress.EnemySpawnerPath);

			EnemySpawner enemySpawner = Object.Instantiate(prefab, at, Quaternion.identity).GetComponent<EnemySpawner>();

			enemySpawner.Construct(this);
			enemySpawner.Type = type;
			enemySpawner.Spawn();
		}
	}
}