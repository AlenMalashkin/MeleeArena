using Code.Data;
using Code.Enemy;
using Code.StaticData;
using Code.UI.Windows;

namespace Code.Services.StaticData
{
	public interface IStaticDataService : IService
	{
		void Load();
		EnemyStaticData ForEnemy(EnemyType type);
		LevelStaticData ForLevel(string levelName);
		WindowConfig ForWindow(WindowType type);
	}
}