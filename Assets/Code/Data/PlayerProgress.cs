using System;
using System.Collections.Generic;
using Code.Infrastructure.Assets;

namespace Code.Data
{
	[Serializable]
	public class PlayerProgress
	{
		public int Wave = 1;
		public PlayerStats PlayerStats = new PlayerStats();
		public string EquippedItem = AssetAddress.BronzeSword;
		public List<string> BoughtItems = new List<string>()
		{
			AssetAddress.BronzeSword
		};
	}
}