using System.Threading.Tasks;
using Code.CameraLogic;
using Code.Enemy;
using Code.Infrastructure.Assets;
using Code.Infrastructure.Factory;
using Code.Logic;
using Code.Services.StaticData;
using Code.StaticData;
using Code.UI.Elements;
using Code.UI.LoadingCurtain;
using Code.UI.Services.Factory;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.GameStates
{
	public class LoadLevelState : IPayloadedState<string>
	{
		private IGameStateMachine _gameStateMachine;
		private SceneLoader _sceneLoader;
		private LoadingCurtain _curtain;
		private IGameFactory _gameFactory;
		private IAssetProvider _assetProvider;
		private IStaticDataService _staticDataService;
		private IUIFactory _uiFactory;

		private GameObject _playerGameObject;
			
		public LoadLevelState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader,
			LoadingCurtain curtain, IGameFactory gameFactory, IAssetProvider assetProvider,
			IStaticDataService staticDataService, IUIFactory uiFactory)
		{
			_gameStateMachine = gameStateMachine;
			_sceneLoader = sceneLoader;
			_curtain = curtain;
			_gameFactory = gameFactory;
			_assetProvider = assetProvider;
			_staticDataService = staticDataService;
			_uiFactory = uiFactory;
		}
		
		public void Enter(string sceneName)
		{
			Cleanup();
			_curtain.Show();
			_sceneLoader.Load("Main", OnLoaded);
		}

		public void Exit()
			=> _curtain.Hide();

		private async void OnLoaded()
		{
			await InitGameWorld();
			_gameStateMachine.Enter<GameState, GameObject>(_playerGameObject);
		}

		private async Task<GameObject> InitPlayer(Vector3 at)
		{
			return await _gameFactory.CreatePlayer(at);
		}

		private async Task<GameObject> InitHud(IHealth health)
		{
			GameObject hud = await _gameFactory.CreateHud();
			hud.GetComponentInChildren<ActorUI>().Construct(health);
			return hud;
		}

		private async Task InitEnemySpawners(LevelStaticData levelStaticData)
		{
			foreach (var enemySpawner in levelStaticData.EnemySpawners)
			{
				await _gameFactory.CreateSpawner(enemySpawner.EnemyType, enemySpawner.Position);
			}
		}

		private async Task InitUIRoot()
		{
			await _uiFactory.CreateUIRoot();
		}

		private async Task InitGameWorld()
		{
			LevelStaticData levelStaticData = LevelStaticData();

			await InitEnemySpawners(levelStaticData);
			_playerGameObject = await InitPlayer(levelStaticData.PlayerPositionOnLevel);
			await InitUIRoot();
			await InitHud(_playerGameObject.GetComponent<IHealth>());
			CameraFollow();
		}

		private LevelStaticData LevelStaticData()
			=> _staticDataService.ForLevel(SceneManager.GetActiveScene().name);

		private void CameraFollow()
			=> Camera.main.GetComponent<FollowingCamera>().Follow(_playerGameObject);
		
		private void Cleanup()
		{
			_assetProvider.CleanUp();
		}
	}
}