using System.Collections.Generic;
using Code.Enemy;

namespace Code.Data
{
	public static class WaveSettingsByWaveType
	{
		private static readonly Dictionary<WaveType, WaveSettings> _waveSettingsMap = new Dictionary<WaveType, WaveSettings>()
		{
			[WaveType.DefaultWave] = new WaveSettings(EnemyType.Default, 7, 10),
			[WaveType.BlueWave] = new WaveSettings(EnemyType.Blue, 7, 12),
			[WaveType.RedWave] = new WaveSettings(EnemyType.Red, 6, 15),
			[WaveType.GreenWave] = new WaveSettings(EnemyType.Green, 6, 18),
			[WaveType.GoldWave] = new WaveSettings(EnemyType.Gold, 5, 20),
			[WaveType.PurpleWave] = new WaveSettings(EnemyType.Purple, 5, 999999)
		};

		public static WaveSettings GetWaveSettingsByWaveType(int wave)
			=> _waveSettingsMap[WaveTypeByWave.GetWaveTypeByWave(wave)];
	}
}