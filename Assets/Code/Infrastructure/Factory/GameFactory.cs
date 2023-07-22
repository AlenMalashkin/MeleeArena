using System.Threading.Tasks;
using Code.Data;
using Code.Enemy;
using Code.Infrastructure.Assets;
using Code.Infrastructure.GameStates;
using Code.Logic;
using Code.Logic.GameplayObjects;
using Code.Logic.Spawners;
using Code.Player;
using Code.Services.GameplayObjectsService;
using Code.Services.KillCountService;
using Code.Services.PersistentProgress;
using Code.Services.StaticData;
using Code.Services.WaveService;
using Code.StaticData;
using Code.UI.Elements;
using Code.UI.Windows.ShopWindow;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
	public class GameFactory : IGameFactory
	{
		private IAssetProvider _assetProvider;
		private IGameStateMachine _gameStateMachine;
		private IStaticDataService _staticDataService;
		private IPersistentProgressService _persistentProgressService;
		private IKillCountService _killCountService;
		private IGameResultReporterService _gameResultReporterService;
		private IWaveService _waveService;
		
		private GameObject _playerGameObject;

		public GameFactory(IAssetProvider assetProvider, IGameStateMachine gameStateMachine, IStaticDataService staticDataService, 
			IPersistentProgressService persistentProgressService, IKillCountService killCountService, 
			IGameResultReporterService gameResultReporterService, IWaveService waveService)
		{
			_assetProvider = assetProvider;
			_gameStateMachine = gameStateMachine;
			_staticDataService = staticDataService;
			_persistentProgressService = persistentProgressService;
			_killCountService = killCountService;
			_gameResultReporterService = gameResultReporterService;
			_waveService = waveService;
		}

		public async Task<GameObject> CreatePlayer(Vector3 at)
		{
			_playerGameObject = await _assetProvider.Instantiate(AssetAddress.PlayerPath, at);
			
			GameObject swordObject = await CreatePlayerSword(_playerGameObject.GetComponent<PlayerEquipment>().SwordSpawnPoint);
			PlayerSword playerSword = swordObject.GetComponent<PlayerSword>();
			
			_playerGameObject.GetComponent<Player.Player>().Construct(_gameResultReporterService);
			_playerGameObject.GetComponent<PlayerAttack>().Construct(_persistentProgressService.Progress.PlayerStats.Damage, playerSword);
			_playerGameObject.GetComponent<PlayerDeath>().Construct(_gameStateMachine);
			
			return _playerGameObject;
		}

		public async Task<GameObject> CreatePlayerSword(Transform under)
			=> await _assetProvider.Instantiate(_persistentProgressService.Progress.EquippedItem, under);

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

		public async Task<GameObject> CreateSpawner(Vector3 at)
		{
			GameObject prefab = await _assetProvider.Load<GameObject>(AssetAddress.EnemySpawnerPath);
			EnemySpawner enemySpawner = Object.Instantiate(prefab, at, Quaternion.identity).GetComponent<EnemySpawner>();
			
			enemySpawner.Construct(this, _killCountService, _gameResultReporterService);
			enemySpawner.transform.position = at;
			enemySpawner.Type = WaveSettingsByWaveType.GetWaveSettingsByWaveType(_waveService.Wave).Type;
			enemySpawner.TimeToRespawn = WaveSettingsByWaveType.GetWaveSettingsByWaveType(_waveService.Wave).SpawnDuration;
			enemySpawner.Spawn();
			return enemySpawner.gameObject;
		}
	}
}