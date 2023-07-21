using Code.Enemy;
using Code.Infrastructure.Factory;
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

        public void Construct(IGameFactory gameFactory, IKillCountService killCountService, IGameResultReporterService gameResultReporterService)
        {
            _gameFactory = gameFactory;
            _killCountService = killCountService;
            _timer = new SyncedTimer(TimerType.UpdateTick);
            _gameResultReporterService = gameResultReporterService;
            _gameResultReporterService.ResultsReported += OnResultsReported;
            _timer.TimerFinished += Spawn;
        }

        private void OnResultsReported(GameResults results)
        {
            _gameResultReporterService.ResultsReported -= OnResultsReported;
            _timer.TimerFinished -= Spawn;
            _timer.Stop();
        }

        public async void Spawn()
        {
            GameObject enemy = await _gameFactory.CreateEnemy(Type, transform.position);
            _enemyDeath = enemy.GetComponent<EnemyDeath>();
            _enemyDeath.EnemyDied += OnEnemyDied;
        }

        private void OnEnemyDied()
        {
            _timer.Start(TimeToRespawn);
            _killCountService.CountKill();
            if (_enemyDeath != null)
                _enemyDeath.EnemyDied -= OnEnemyDied;
        }
    }
}