using System;
using Code.Enemy;
using UnityEngine;

namespace Code.Data
{
	[Serializable]
	public class EnemySpawnerData
	{
		public Vector3 Position;

		public EnemySpawnerData(Vector3 position)
		{
			Position = position;
		}
	}
}