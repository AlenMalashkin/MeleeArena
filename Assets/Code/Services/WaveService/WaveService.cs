using Code.Services.GameplayObjectsService;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoadService;

namespace Code.Services.WaveService
{
	public class WaveService : IWaveService
	{
		private ISaveLoadService _saveLoadService;
		private IPersistentProgressService _persistentProgress;
		private IGameResultReporterService _gameResultReporter;

		public WaveService(ISaveLoadService saveLoadService, IPersistentProgressService persistentProgress)
		{
			_saveLoadService = saveLoadService;
			_persistentProgress = persistentProgress;
		}

		public int Wave
		{
			get => _persistentProgress.Progress.Wave;
			private set => _persistentProgress.Progress.Wave = value;
		}

		public void PassWave()
		{
			Wave++;
			_saveLoadService.Save();
		}
	}
}