using System.Threading.Tasks;
using Code.Enemy;
using Code.Services;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
	public interface IGameFactory : IService
	{
		Task<GameObject> CreatePlayer(Vector3 at);
		Task<GameObject> CreatePlayerSword(Transform under);
		Task<GameObject> CreateHud();
		Task<GameObject> CreateEnemy(EnemyType type, Vector3 at);
		Task<GameObject> CreateSpawner(Vector3 at);
	}
}