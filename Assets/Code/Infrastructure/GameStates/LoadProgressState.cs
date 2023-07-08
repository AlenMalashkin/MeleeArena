using Code.Data;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoadService;

namespace Code.Infrastructure.GameStates
{
	public class LoadProgressState : IState
	{
		private IGameStateMachine _gameStateMachine;
		private IPersistentProgressService _persistentProgressService;
		private ISaveLoadService _saveLoadService;
		
		public LoadProgressState(IGameStateMachine gameStateMachine, IPersistentProgressService persistentProgressService, ISaveLoadService saveLoadService)
		{
			_gameStateMachine = gameStateMachine;
			_persistentProgressService = persistentProgressService;
			_saveLoadService = saveLoadService;
		}
		
		public void Enter()
		{
			LoadProgressOrCreateNew();
			
			_gameStateMachine.Enter<LoadLevelState, string>("Main");
		}

		public void Exit()
		{
			
		}

		private void LoadProgressOrCreateNew()
		{
			_persistentProgressService.Progress = _saveLoadService.Load() ?? CreateNewProgress();
		}

		private PlayerProgress CreateNewProgress()
		{
			return new PlayerProgress();			
		}
	}
}