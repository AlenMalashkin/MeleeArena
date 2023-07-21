using System;

namespace Code.Services.Bank
{
	public interface IBank : IService
	{
		event Action<int> MoneyChanged;
		int Money { get; }
		void GetMoney(int amount);
		bool SpendMoney(int amount);
	}
}