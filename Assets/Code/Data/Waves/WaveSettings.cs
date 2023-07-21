using Code.Enemy;

namespace Code.Data
{
	public class WaveSettings
	{
		public EnemyType Type;
		public float SpawnDuration;
		public int KillCount;

		public WaveSettings(EnemyType type, float spawnDuration, int killCount)
		{
			Type = type;
			SpawnDuration = spawnDuration;
			KillCount = killCount;
		}
	}
}