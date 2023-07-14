using System;
using Code.Services.GameOverReporterService;

namespace Code.Services.GameplayObjectsService
{
	public interface IGameResultReporterService : IService
	{
		event Action<GameResults> ResultsReported;
		void ReportResults(GameResults results);
	}
}