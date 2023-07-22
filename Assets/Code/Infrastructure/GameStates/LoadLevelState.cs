using System.Threading.Tasks;
using Code.CameraLogic;
using Code.Infrastructure.Assets;
using Code.Infrastructure.Factory;
using Code.Logic;
using Code.Player;
using Code.Services.PersistentProgress;
using Code.Services.StaticData;
using Code.StaticData;
using Code.UI.Elements;
using Code.UI.LoadingCurtain;
using Code.UI.Services.Factory;
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

		private GameObject _player;

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
			_gameStateMachine.Enter<GameState>();
		}

		private async Task<GameObject> InitPlayer(Vector3 at)
		{
			GameObject player = await _gameFactory.CreatePlayer(at);
			return player;
		}

		private async Task InitHud(GameObject playerGameObject)
		{
			GameObject hud = await _gameFactory.CreateHud();
			hud.GetComponentInChildren<ActorUI>().Construct(playerGameObject.GetComponent<IHealth>());
		}

		private async Task InitEnemySpawners(LevelStaticData levelStaticData)
		{
			foreach (var enemySpawner in levelStaticData.EnemySpawners)
			{
				await _gameFactory.CreateSpawner(enemySpawner.Position);
			}
		}

		private async Task InitUIRoot()
		{
			await _uiFactory.CreateUIRoot();
		}

		private async Task InitGameWorld()
		{
			LevelStaticData levelStaticData = LevelStaticData();

			_player = await InitPlayer(levelStaticData.PlayerPositionOnLevel);
			await InitHud(_player);
			await InitUIRoot();
			await InitEnemySpawners(levelStaticData);
			CameraFollow();
		}

		private LevelStaticData LevelStaticData()
			=> _staticDataService.ForLevel(SceneManager.GetActiveScene().name);

		private void CameraFollow()
			=> Camera.main.GetComponent<FollowingCamera>().Follow(_player);
		
		private void Cleanup()
		{
			_assetProvider.CleanUp();
		}
	}
}