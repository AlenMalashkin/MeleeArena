using System;
using Code.Services.GameOverReporterService;
using Code.Services.GameplayObjectsService;

namespace Code.Logic.GameplayObjects
{
	public class GameResultReporterService : IGameResultReporterService
	{
		public event Action<GameResults> ResultsReported;
		public void ReportResults(GameResults results)
		{
			ResultsReported?.Invoke(results);
		}
	}
}