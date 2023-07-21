using System.Collections.Generic;

namespace Code.Data
{
	public class WaveTypeByWave
	{
		private static readonly Dictionary<(int start, int end), WaveType> _waveTypeMap = new Dictionary<(int start, int end), WaveType>()
		{
			[(1, 5)] = WaveType.DefaultWave,
			[(6, 10)] = WaveType.BlueWave,
			[(11, 15)] = WaveType.RedWave,
			[(16, 20)] = WaveType.GreenWave,
			[(21, 25)] = WaveType.GoldWave,
			[(26, 999999)] = WaveType.PurpleWave
		};

		public static WaveType GetWaveTypeByWave(int wave)
		{
			foreach (var kvp in _waveTypeMap)
			{
				if (wave >= kvp.Key.start && wave <= kvp.Key.end)
				{
					return kvp.Value;
				}
			}

			return WaveType.DefaultWave;
		}
	}
}