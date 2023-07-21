using System;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoadService;
using UnityEngine;

namespace Code.Services.Bank
{
	public class Bank : IBank
	{
		public event Action<int> MoneyChanged;

		private ISaveLoadService _saveLoadService;
		private IPersistentProgressService _persistentProgress;
		
		public Bank(ISaveLoadService saveLoadService, IPersistentProgressService persistentProgress)
		{
			_saveLoadService = saveLoadService;
			_persistentProgress = persistentProgress;
		}
		
		public int Money
		{
			get => _persistentProgress.Progress.PlayerStats.Money;
			private set => _persistentProgress.Progress.PlayerStats.Money = value;
		}
		
		public void GetMoney(int amount)
		{
			Money += amount;
			MoneyChanged?.Invoke(Money);
			_saveLoadService.Save();
		}

		public bool SpendMoney(int amount)
		{
			if (Money < amount) 
				return false;
			
			Money -= amount;
			_saveLoadService.Save();
			MoneyChanged?.Invoke(Money);
			return true;
		}
	}
}