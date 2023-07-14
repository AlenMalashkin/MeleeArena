using System.Threading.Tasks;
using Code.Enemy;
using Code.Infrastructure.Assets;
using Code.Logic;
using Code.Logic.GameplayObjects;
using Code.Logic.Spawners;
using Code.Player;
using Code.Services.GameplayObjectsService;
using Code.Services.KillCountService;
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
		private IKillCountService _killCountService;
		private IGameResultReporterService _gameResultReporterService;
		
		private GameObject _playerGameObject;

		public GameFactory(IAssetProvider assetProvider, IStaticDataService staticDataService,
			IKillCountService killCountService, IGameResultReporterService gameResultReporterService)
		{
			_assetProvider = assetProvider;
			_staticDataService = staticDataService;
			_killCountService = killCountService;
			_gameResultReporterService = gameResultReporterService;
		}

		public async Task<GameObject> CreatePlayer(Vector3 at)
		{
			_playerGameObject = await _assetProvider.Instantiate(AssetAddress.PlayerPath);
			_playerGameObject.GetComponent<Player.Player>().Construct(_gameResultReporterService);
			PlayerHealth playerHealth = _playerGameObject.GetComponent<PlayerHealth>();
			playerHealth.MaxHealth = 10;
			playerHealth.CurrentHealth = 10;
			return _playerGameObject;
		}

		public async Task<GameObject> CreatePlayerSword(Transform under)
			=> await _assetProvider.Instantiate(AssetAddress.BronzeSword, under);

		public async Task<GameObject> CreateHud()
		{
			GameObject hud = await _assetProvider.Instantiate(AssetAddress.HudPath);
			hud.GetComponent<Hud>().Construct(_gameResultReporterService);
			return hud;
		}

		public async Task<GameObject> CreateEnemy(EnemyType type, Vector3 at)
		{
			EnemyStaticData enemyStaticData = _staticDataService.ForEnemy(type);
			GameObject prefab = await _assetProvider.Load<GameObject>(enemyStaticData.AssetReferenceGameObject);

			GameObject enemy = Object.Instantiate(prefab, at, Quaternion.identity);

			enemy.GetComponent<Enemy.Enemy>().Construct(_gameResultReporterService);
			enemy.GetComponent<EnemyMovement>().Construct(enemyStaticData.MoveSpeed, _playerGameObject.transform);
			enemy.GetComponent<EnemyHealth>().Construct(enemyStaticData.Health, enemyStaticData.Health);
			enemy.GetComponent<ActorUI>().Construct(enemy.GetComponent<IHealth>());
			enemy.GetComponent<EnemyAttack>().Construct(_playerGameObject.transform, enemyStaticData.Damage, enemyStaticData.AttackCooldown, enemyStaticData.AttackDistance, enemyStaticData.Cleavage);
			
			return enemy;
		}

		public async Task<GameObject> CreateSpawner(EnemyType type, Vector3 at, float timeToRespawn)
		{
			GameObject prefab = await _assetProvider.Load<GameObject>(AssetAddress.EnemySpawnerPath);
			EnemySpawner enemySpawner = prefab.GetComponent<EnemySpawner>();
			
			enemySpawner.Construct(this, _killCountService, _gameResultReporterService);
			enemySpawner.transform.position = at;
			enemySpawner.Type = type;
			enemySpawner.TimeToRespawn = timeToRespawn;
			enemySpawner.Spawn();
			return enemySpawner.gameObject;
		}
	}
}