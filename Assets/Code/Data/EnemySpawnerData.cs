using System;
using Code.Enemy;
using UnityEngine;

namespace Code.Data
{
	[Serializable]
	public class EnemySpawnerData
	{
		public EnemyType EnemyType;
		public Vector3 Position;
		public float TimeToRespawn;

		public EnemySpawnerData(EnemyType type, Vector3 position, float timeToRespawn)
		{
			EnemyType = type;
			Position = position;
			TimeToRespawn = timeToRespawn;
		}
	}
}