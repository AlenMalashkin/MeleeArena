using System.Collections.Generic;
using Code.Infrastructure.Assets;
using Code.UI.Windows.ShopWindow;

namespace Code.Data
{
	public static class ItemAddressByType
	{
		private static readonly Dictionary<ItemType, string> itemAddressesMap = new Dictionary<ItemType, string>()
		{
			[ItemType.BronzeSword] = AssetAddress.BronzeSword,
			[ItemType.SilverSword] = AssetAddress.SilverSword,
			[ItemType.RedSword] = AssetAddress.RedSword
		};
		
		public static string GetItemAddressByType(ItemType type)
			=> itemAddressesMap[type];
	}
}