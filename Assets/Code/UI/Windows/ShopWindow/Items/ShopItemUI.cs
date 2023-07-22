using System;
using Code.Data;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.UI.Elements;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.ShopWindow
{
	public class ShopItemUI : MonoBehaviour, IUpdatableUI
	{
		[SerializeField] private Button buyOrEquipButton;
		[SerializeField] private Image image;
		[SerializeField] private TextMeshProUGUI shopItemText;

		private IPersistentProgressService _persistentProgress;
		private ItemData _data;
		private bool _isBought;
		private bool _isEquipped;

		public Button BuyOrEquipButton => buyOrEquipButton;

		public void Construct(IPersistentProgressService persistentProgress, ItemData data)
		{
			_persistentProgress = persistentProgress;
			_data = data;

			image.sprite = _data.Sprite;
		}
		
		public void UpdateUI()
		{
			_isBought = _persistentProgress.Progress.BoughtItems.Exists(x =>
				x == ItemAddressByType.GetItemAddressByType(_data.Type));

			_isEquipped = _persistentProgress.Progress.EquippedItem ==
			             ItemAddressByType.GetItemAddressByType(_data.Type);
			
			if (!_isEquipped && _isBought)
				BougthUI();
			else if (_isBought && _isEquipped)
				EquippedUI();
			else
				NotBougthUI();
		}

		private void UpdateButton(bool interactable)
		{
			buyOrEquipButton.interactable = interactable;
		}

		private void UpdateText(string text)
		{
			shopItemText.text = text;
		}

		private void UpdateImage(Color color)
		{
			image.color = color;
		}

		private void NotBougthUI()
		{
			UpdateButton(true);
			UpdateText(_data.Cost + "");
			UpdateImage(new Color(255, 255, 255, 100));
		}

		private void BougthUI()
		{
			UpdateButton(true);
			UpdateText("Выбрать");
			UpdateImage(new Color(255, 255, 255, 255));
		}

		private void EquippedUI()
		{
			UpdateButton(false);
			UpdateText("Выбрано");
			UpdateImage(new Color(255, 255, 255, 150));
		}
	}
}