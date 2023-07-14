using System;

namespace Code.Services.KillCountService
{
	public interface IKillCountService : IService
	{
		event Action<int> KillCountChanged;
		int KillCount { get; }
		void CountKill();
		void Reset();
	}
}