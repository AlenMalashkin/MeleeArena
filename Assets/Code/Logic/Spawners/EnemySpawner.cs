using System;
using Code.Enemy;
using Code.Infrastructure.Factory;
using UnityEngine;

namespace Code.Logic.Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        public EnemyType Type;

        private IGameFactory _gameFactory;

        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public async void Spawn()
        {
            await _gameFactory.CreateEnemy(EnemyType.Default, transform.position);
        }
    }
}