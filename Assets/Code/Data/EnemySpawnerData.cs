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

		public EnemySpawnerData(EnemyType type, Vector3 position)
		{
			EnemyType = type;
			Position = position;
		}
	}
}