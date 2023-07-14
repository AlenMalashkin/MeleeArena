using System;
using System.Collections.Generic;

namespace Code.Data
{
	[Serializable]
	public class PlayerProgress
	{
		public string EquippedItem;
		public List<string> BoughtItems = new List<string>();
	}
}