using System.Collections.Generic;
using System.Linq;
using Code.Data;
using Code.Enemy;
using Code.StaticData;
using Code.UI.Windows;
using UnityEngine;

namespace Code.Services.StaticData
{
	public class StaticDataService : IStaticDataService
	{
		private Dictionary<EnemyType, EnemyStaticData> _enemyStaticDataMap;
		private Dictionary<string, LevelStaticData> _levelStaticDataMap;
		private Dictionary<WindowType, WindowConfig> _windowConfigs;
		public void Load()
		{
			_enemyStaticDataMap = Resources.LoadAll<EnemyStaticData>("StaticData/Enemies")
				.ToDictionary(x => x.Type, x => x);

			_levelStaticDataMap = Resources.LoadAll<LevelStaticData>("StaticData/Levels")
				.ToDictionary(x => x.LevelName, x => x);

			_windowConfigs = Resources.Load<WindowStaticData>("StaticData/Windows/WindowStaticData")
				.Configs
				.ToDictionary(x => x.Type, x => x);
		}

		public EnemyStaticData ForEnemy(EnemyType type)
			=> _enemyStaticDataMap.TryGetValue(type, out EnemyStaticData staticData)
				? staticData
				: null;

		public LevelStaticData ForLevel(string levelName)
			=> _levelStaticDataMap.TryGetValue(levelName, out LevelStaticData staticData)
				? staticData
				: null;

		public WindowConfig ForWindow(WindowType type)
			=> _windowConfigs.TryGetValue(type, out WindowConfig config)
				? config
				: null;
	}
}