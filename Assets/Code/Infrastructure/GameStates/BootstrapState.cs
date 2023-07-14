using Code.Infrastructure.Assets;
using Code.Infrastructure.Factory;
using Code.Logic.GameplayObjects;
using Code.Services;
using Code.Services.GameplayObjectsService;
using Code.Services.Input;
using Code.Services.KillCountService;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoadService;
using Code.Services.StaticData;
using Code.UI.LoadingCurtain;
using Code.UI.Services.Factory;
using Code.UI.Services.WindowService;
using UnityEngine;

namespace Code.Infrastructure.GameStates
{
	public class BootstrapState : IState
	{
		private const string Initial = "Initial";
		
		private IGameStateMachine _gameStateMachine;
		private SceneLoader _sceneLoader;
		private ServiceLocator _serviceLocator;
		private LoadingCurtain _loadingCurtain;
		
		public BootstrapState(IGameStateMachine gameStateMachine, LoadingCurtain loadingCurtain, SceneLoader sceneLoader, ServiceLocator serviceLocator)
		{
			_gameStateMachine = gameStateMachine;
			_sceneLoader = sceneLoader;
			_serviceLocator = serviceLocator;
			_loadingCurtain = loadingCurtain;
			
			RegisterAllServices();
		}

		public void Enter()
			=> _sceneLoader.Load(Initial, OnLoad);
		
		public void Exit()
		{
		}

		private void OnLoad()
			=> _gameStateMachine.Enter<LoadProgressState>();

		private void RegisterAllServices()
		{
			RegisterAssetProvider();
			RegisterStaticDataService();

			_serviceLocator.RegisterService(_gameStateMachine);
			_serviceLocator.RegisterService<IInputService>(InitInputService());
			_serviceLocator.RegisterService<IKillCountService>(new KillCountService());
			_serviceLocator.RegisterService<IGameResultReporterService>(new GameResultReporterService());
			_serviceLocator.RegisterService<IGameFactory>(new GameFactory(
				_serviceLocator.Resolve<IAssetProvider>(),
				_serviceLocator.Resolve<IStaticDataService>(),
				_serviceLocator.Resolve<IKillCountService>(), _serviceLocator.Resolve<IGameResultReporterService>())
			);
			_serviceLocator.RegisterService<IUIFactory>(new UIFactory
			(
				_serviceLocator.Resolve<IGameStateMachine>(),
				_serviceLocator.Resolve<IAssetProvider>(),
				_serviceLocator.Resolve<IStaticDataService>(),
				_loadingCurtain
			));
			_serviceLocator.RegisterService<IPersistentProgressService>(new PersistentProgressService());
			_serviceLocator.RegisterService<ISaveLoadService>(new SaveLoadService(_serviceLocator.Resolve<IPersistentProgressService>()));
			_serviceLocator.RegisterService<IWindowService>(new WindowService(_serviceLocator.Resolve<IUIFactory>()));
		}

		private void RegisterAssetProvider()
		{
			AssetProvider assetProvider = new AssetProvider();
			_serviceLocator.RegisterService<IAssetProvider>(assetProvider);
			assetProvider.Initialize();
		}

		private void RegisterStaticDataService()
		{
			StaticDataService staticDataService = new StaticDataService();
			staticDataService.Load();
			_serviceLocator.RegisterService<IStaticDataService>(staticDataService);
		}
		
		private static IInputService InitInputService()
			=> Application.isEditor
				? (IInputService) new DesctopInputService()
				: new MobileInputService();
	}
}