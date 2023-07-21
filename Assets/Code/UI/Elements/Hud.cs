using Code.Services.GameOverReporterService;
using Code.Services.GameplayObjectsService;
using UnityEngine;

namespace Code.UI.Elements
{
	public class Hud : MonoBehaviour
	{
		private IGameResultReporterService _gameResultReporterService;
		
		public void Construct(IGameResultReporterService gameResultReporterService)
		{
			_gameResultReporterService = gameResultReporterService;
			_gameResultReporterService.ResultsReported += OnResultsReported;
		}
		
		private void OnResultsReported(GameResults results)
		{
			Destroy(gameObject);
			_gameResultReporterService.ResultsReported -= OnResultsReported;
		}
	}
}