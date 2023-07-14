using System;
using Code.Services.GameOverReporterService;
using Code.Services.GameplayObjectsService;
using UnityEngine;

namespace Code.Player
{
	public class Player : MonoBehaviour
	{
		[SerializeField] private PlayerAnimator animator;
		[SerializeField] private PlayerMovement playerMovement;
		
		private IGameResultReporterService _gameResultReporterService;
		
		public void Construct(IGameResultReporterService gameResultReporterService)
		{
			_gameResultReporterService = gameResultReporterService;
			_gameResultReporterService.ResultsReported += OnResultsReported;
		}
		
		private void OnResultsReported(GameResults results)
		{
			if (results == GameResults.Win)
				OnWinResult();
			
			playerMovement.enabled = false;
			_gameResultReporterService.ResultsReported -= OnResultsReported;
		}

		private void OnWinResult()
		{
			animator.PlayVictory();
		}
	}
}