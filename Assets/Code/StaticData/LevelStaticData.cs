using System.Collections.Generic;
using Code.Data;
using UnityEngine;

namespace Code.StaticData
{
	[CreateAssetMenu(fileName = "LevelStaticData", menuName = "LevelStaticData", order = 2)]
	public class LevelStaticData : ScriptableObject
	{
		public string LevelName;
		public List<EnemySpawnerData> EnemySpawners;
		public Vector3 PlayerPositionOnLevel;
	}
}