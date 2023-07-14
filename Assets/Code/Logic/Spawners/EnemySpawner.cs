using System;
using System.Collections.Generic;
using Code.Enemy;
using Code.Infrastructure.Factory;
using Code.Logic.GameplayObjects;
using Code.Services.GameOverReporterService;
using Code.Services.GameplayObjectsService;
using Code.Services.KillCountService;
using UnityEngine;
using VavilichevGD.Utils.Timing;

namespace Code.Logic.Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        public EnemyType Type;
        public float TimeToRespawn;
        
        private IGameFactory _gameFactory;
        private IKillCountService _killCountService;
        private EnemyDeath _enemyDeath;
        private IGameResultReporterService _gameResultReporterService;
        private SyncedTimer _timer;
        private bool _canSpawn;

        public void Construct(IGameFactory gameFactory, IKillCountService killCountService, IGameResultReporterService gameResultReporterService)
        {
            _gameFactory = gameFactory;
            _killCountService = killCountService;
            _timer = new SyncedTimer(TimerType.UpdateTick);
            _gameResultReporterService = gameResultReporterService;
            _gameResultReporterService.ResultsReported += OnResultsReported;
            _canSpawn = true;
        }

        private void OnResultsReported(GameResults results)
        {
            _canSpawn = false;
            _gameResultReporterService.ResultsReported -= OnResultsReported;
            _timer.TimerFinished -= Respawn;
            _timer.Stop();
        }

        public async void Spawn()
        {
            if (_canSpawn)
            {
                GameObject enemy = await _gameFactory.CreateEnemy(EnemyType.Default, transform.position);
                _enemyDeath = enemy.GetComponent<EnemyDeath>();
                _enemyDeath.EnemyDied += _killCountService.CountKill;
                _timer.Start(TimeToRespawn);
                _timer.TimerFinished += Respawn;
            }
            
        }

        private void Respawn()
        {
            Spawn();
            _enemyDeath.EnemyDied -= _killCountService.CountKill;
            _timer.TimerFinished -= Respawn;
        }
    }
}