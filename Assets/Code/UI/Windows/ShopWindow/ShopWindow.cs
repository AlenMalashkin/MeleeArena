using System;
using System.Collections.Generic;
using Code.Services;
using Code.Services.Bank;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoadService;
using Code.StaticData;
using Code.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.ShopWindow
{
	public class ShopWindow : WindowBase
	{
		[SerializeField] private ItemStaticData itemStaticData;
		[SerializeField] private ShopItemBase itemBase;
		[SerializeField] private Button back;
		[SerializeField] private Transform itemContainer;

		private List<IUpdatableUI> _updatableShopItems = new List<IUpdatableUI>();
		private IBank _bank;
		private ISaveLoadService _saveLoadService;
		private IPersistentProgressService _persistentProgress;
		
		protected override void OnAwake()
		{
			_bank = ServiceLocator.Container.Resolve<IBank>();
			_saveLoadService = ServiceLocator.Container.Resolve<ISaveLoadService>();
			_persistentProgress = ServiceLocator.Container.Resolve<IPersistentProgressService>();

			foreach (var itemData in itemStaticData.ItemDatas)
			{
				SwordItem item = Instantiate(itemBase, itemContainer) as SwordItem;
				_updatableShopItems.Add(item.GetComponent<IUpdatableUI>());
				item.Construct(itemData, _bank, _saveLoadService, _persistentProgress, this);
			}
		}

		public void UpdateShopItemsUI()
		{
			foreach (var updatableShopItem in _updatableShopItems)
			{
				updatableShopItem.UpdateUI();
			}
		}

		private void OnEnable()
		{
			back.onClick.AddListener(Close);
		}

		private void OnDisable()
		{
			back.onClick.RemoveListener(Close);
		}

		private void Close()
			=> Destroy(gameObject);
	}
}