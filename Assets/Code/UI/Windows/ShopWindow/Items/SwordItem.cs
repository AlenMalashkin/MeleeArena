using Code.Data;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoadService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.ShopWindow
{
	public class SwordItem : ShopItemBase
	{
		[SerializeField] private Button buyOrEquipButton;
		[SerializeField] private Image image;
		[SerializeField] private TextMeshProUGUI cost;
		
		private ISaveLoadService _saveLoadService;
		private IPersistentProgressService _persistentProgress;
		private ItemData _itemData;

		public void Construct(ItemData itemData, ISaveLoadService saveLoadService, IPersistentProgressService persistentProgressService)
		{
			_itemData = itemData;
			_saveLoadService = saveLoadService;
			_persistentProgress = persistentProgressService;

			IsBougth = _persistentProgress.Progress.BoughtItems.Exists(x =>
				x == ItemAddressByType.GetItemAddressByType(_itemData.Type));

			if (IsBougth)
			{
				image.sprite = _itemData.Sprite;
				image.color = new Color(255, 255, 255, 255);
				cost.text = "Equip";
			}
			else
			{
				image.sprite = _itemData.Sprite;
				image.color = new Color(255, 255, 255, 100);
				cost.text = _itemData.Cost + "";
			}
			
			buyOrEquipButton.onClick.AddListener(Buy);
			buyOrEquipButton.onClick.AddListener(Equip);
		}

		protected override void Buy()
		{
			if (_persistentProgress.Progress.BoughtItems.Exists(x => 
				x == ItemAddressByType.GetItemAddressByType(_itemData.Type))) 
				return;
			
			_persistentProgress.Progress.BoughtItems.Add(ItemAddressByType.GetItemAddressByType(_itemData.Type));
			_saveLoadService.Save();
		}

		protected override void Equip()
		{
			_persistentProgress.Progress.EquippedItem = ItemAddressByType.GetItemAddressByType(_itemData.Type);
		}
	}
}