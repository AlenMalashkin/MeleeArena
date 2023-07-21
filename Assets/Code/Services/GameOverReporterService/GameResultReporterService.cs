using System;
using Code.Services.GameOverReporterService;
using Code.Services.GameplayObjectsService;
using Code.Services.WaveService;

namespace Code.Logic.GameplayObjects
{
	public class GameResultReporterService : IGameResultReporterService
	{
		private IWaveService _waveService;
		
		public GameResultReporterService(IWaveService waveService)
		{
			_waveService = waveService;
		}

		public event Action<GameResults> ResultsReported;
		public void ReportResults(GameResults results)
		{
			ResultsReported?.Invoke(results);
			
			if (results == GameResults.Win)
				_waveService.PassWave();
		}
	}
}