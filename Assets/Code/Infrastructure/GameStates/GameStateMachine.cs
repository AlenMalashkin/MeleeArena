using System;
using System.Collections.Generic;
using Code.Infrastructure.Assets;
using Code.Infrastructure.Factory;
using Code.Services;
using Code.Services.GameplayObjectsService;
using Code.Services.KillCountService;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoadService;
using Code.Services.StaticData;
using Code.Services.WaveService;
using Code.UI.LoadingCurtain;
using Code.UI.Services.Factory;
using Code.UI.Services.WindowService;

namespace Code.Infrastructure.GameStates
{
	public class GameStateMachine : IGameStateMachine
	{
		private Dictionary<Type, IExitableState> _stateMap;
		private IExitableState _currentState;

		public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, ServiceLocator serviceLocator)
		{
			_stateMap = new Dictionary<Type, IExitableState>
			{
				[typeof(BootstrapState)] = new BootstrapState(this, loadingCurtain, sceneLoader, serviceLocator),
				[typeof(MenuState)] = new MenuState(sceneLoader, loadingCurtain, serviceLocator.Resolve<IUIFactory>()),
				[typeof(LoadProgressState)] = new LoadProgressState(this, serviceLocator.Resolve<IPersistentProgressService>(), serviceLocator.Resolve<ISaveLoadService>()),
				[typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingCurtain, serviceLocator.Resolve<IGameFactory>(), serviceLocator.Resolve<IAssetProvider>(), serviceLocator.Resolve<IStaticDataService>(), serviceLocator.Resolve<IUIFactory>()),
				[typeof(GameState)] = new GameState(this, serviceLocator.Resolve<IKillCountService>(), serviceLocator.Resolve<IWaveService>()),
				[typeof(GameOverState)] = new GameOverState(serviceLocator.Resolve<IWindowService>(), serviceLocator.Resolve<IGameResultReporterService>()),
			};
		}

		public void Enter<TState>() where TState : class, IState
		{
			IState state = ChangeState<TState>();
			state.Enter();
		}

		public void Enter<TPayloadState, TPayload>(TPayload payload) where TPayloadState : class, IPayloadedState<TPayload>
		{
			TPayloadState payloadedState = ChangeState<TPayloadState>();
			payloadedState.Enter(payload);
		}

		private TState ChangeState<TState>() where TState : class, IExitableState
		{
			_currentState?.Exit();

			TState state = GetState<TState>();
			_currentState = state;
		
			return state;
		}

		private TState GetState<TState>() where TState : class, IExitableState
			=> _stateMap[typeof(TState)] as TState;
	}
}