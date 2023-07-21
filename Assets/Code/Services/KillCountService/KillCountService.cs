using System;
using UnityEngine;

namespace Code.Services.KillCountService
{
	public class KillCountService : IKillCountService
	{
		public event Action<int> KillCountChanged;
		
		public int KillCount { get; private set; }

		public KillCountService()
		{
			KillCount = 0;
		}
		
		public void CountKill()
		{
			KillCount++;
			KillCountChanged?.Invoke(KillCount);
		}

		public void Reset()
		{
			KillCount = 0;
			KillCountChanged?.Invoke(KillCount);
		}
	}
}