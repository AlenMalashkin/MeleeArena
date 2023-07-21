using Code.Data;
using Code.Services.Bank;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoadService;
using Code.UI.Elements;
using UnityEngine;

namespace Code.UI.Windows.ShopWindow
{
	[RequireComponent(typeof(IUpdatableUI))]
	public class SwordItem : ShopItemBase
	{
		[SerializeField] private ShopItemUI shopItemUI;

		private ShopWindow _shopWindow;
		private IBank _bank;
		private ISaveLoadService _saveLoadService;
		private IPersistentProgressService _persistentProgress;
		private ItemData _itemData;

		public void Construct(ItemData itemData, IBank bank, ISaveLoadService saveLoadService,
			IPersistentProgressService persistentProgressService, ShopWindow shopWindow)
		{
			_itemData = itemData;
			_bank = bank;
			_saveLoadService = saveLoadService;
			_persistentProgress = persistentProgressService;
			_shopWindow = shopWindow;

			IsBougth = _persistentProgress.Progress.BoughtItems.Exists(x =>
				x == ItemAddressByType.GetItemAddressByType(_itemData.Type));

			IsEquipped = _persistentProgress.Progress.EquippedItem ==
			             ItemAddressByType.GetItemAddressByType(_itemData.Type);
			
			shopItemUI.Construct(_persistentProgress, _itemData);
			_shopWindow.UpdateShopItemsUI();

			shopItemUI.BuyOrEquipButton.onClick.AddListener(BuyOrEquip);
		}

		private void BuyOrEquip()
		{
			if (!IsBougth)
				Buy();
			else
				Equip();
		}

		protected override void Buy()
		{
			if (_persistentProgress.Progress.BoughtItems.Exists(x => 
				x == ItemAddressByType.GetItemAddressByType(_itemData.Type))) 
				return;

			IsBougth = true;

			_bank.SpendMoney(_itemData.Cost);
			_persistentProgress.Progress.BoughtItems.Add(ItemAddressByType.GetItemAddressByType(_itemData.Type));
			_saveLoadService.Save();
			
			_shopWindow.UpdateShopItemsUI();
		}

		protected override void Equip()
		{
			_persistentProgress.Progress.EquippedItem = ItemAddressByType.GetItemAddressByType(_itemData.Type);
			_persistentProgress.Progress.PlayerStats.Damage = _itemData.Damage;
			_saveLoadService.Save();

			IsEquipped = true;

			_shopWindow.UpdateShopItemsUI();
		}
	}
}