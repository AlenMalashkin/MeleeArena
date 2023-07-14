using System;
using Code.Services.GameOverReporterService;
using Code.Services.GameplayObjectsService;
using UnityEngine;

namespace Code.Enemy
{
	public class Enemy : MonoBehaviour
	{
		[SerializeField] private EnemyAnimator animator;
		
		private IGameResultReporterService _gameResultReporterService;
		
		public void Construct(IGameResultReporterService gameResultReporterService)
		{
			_gameResultReporterService = gameResultReporterService;
			_gameResultReporterService.ResultsReported += OnResultsReported;
		}

		private void OnDestroy()
		{
			_gameResultReporterService.ResultsReported -= OnResultsReported;
		}

		private void OnResultsReported(GameResults results)
		{
			switch (results)
			{
				case GameResults.Win:
					animator.PlayDie();
					break;
				case GameResults.Lose:
					animator.PlayVictory();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(results), results, null);
			}
			
			_gameResultReporterService.ResultsReported -= OnResultsReported;
		}
	}
}