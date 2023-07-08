using Code.Data;
using Code.Extensions.DataExtensions;
using Code.Services.PersistentProgress;
using UnityEngine;

namespace Code.Services.SaveLoadService
{
	public class SaveLoadService : ISaveLoadService
	{
		private const string Key = "Progress";
		
		private IPersistentProgressService _persistentProgressService;
		
		public SaveLoadService(IPersistentProgressService persistentProgressService)
		{
			_persistentProgressService = persistentProgressService;
		}
		
		public void Save()
		{
			PlayerPrefs.SetString(Key, _persistentProgressService.Progress.ToJson());
		}

		public PlayerProgress Load()
			=> PlayerPrefs.GetString(Key).FromJson<PlayerProgress>();
	}
}