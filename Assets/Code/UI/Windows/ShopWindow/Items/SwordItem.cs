using System;
using Code.Data;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoadService;
using Code.StaticData;
using UnityEngine;

namespace Code.UI.Windows.ShopWindow
{
	public class SwordItem : ShopItemBase
	{
		[SerializeField] private ItemStaticData itemStaticData;

		private ISaveLoadService _saveLoadService;
		private IPersistentProgressService _persistentProgress;
		private ItemType _type;
		private string _itemName;

		public void Construct(ISaveLoadService saveLoadService, IPersistentProgressService persistentProgressService)
		{
			_saveLoadService = saveLoadService;
			_persistentProgress = persistentProgressService;
		}

		protected override void OnAwake()
		{
			_type = itemStaticData.Type;
			_itemName = ItemAddressByType.GetItemAddressByType(_type);
		}

		private void OnEnable()
		{
			SubscribeOnBuy();
		}

		private void OnDisable()
		{
			UnsubscribeOnBuy();
		}

		protected override void Buy()
		{
			_persistentProgress.Progress.BoughtItems.Add(_itemName);
			_saveLoadService.Save();
		}
	}
}