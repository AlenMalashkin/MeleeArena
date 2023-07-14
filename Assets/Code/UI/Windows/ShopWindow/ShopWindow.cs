using System;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoadService;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.ShopWindow
{
	public class ShopWindow : WindowBase
	{
		[SerializeField] private ShopItemBase itemBase;
		[SerializeField] private Button back;
		[SerializeField] private Transform itemContainer;

		private ISaveLoadService _saveLoadService;
		private IPersistentProgressService _persistentProgress;
		
		protected override void OnAwake()
		{
			_saveLoadService = ServiceLocator.Container.Resolve<ISaveLoadService>();
			_persistentProgress = ServiceLocator.Container.Resolve<IPersistentProgressService>();
			
			for (int i = 0; i < Enum.GetValues(typeof(ItemType)).Length; i++)
			{
				SwordItem item = Instantiate(itemBase, itemContainer) as SwordItem;
				item.Construct(_saveLoadService, _persistentProgress);
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