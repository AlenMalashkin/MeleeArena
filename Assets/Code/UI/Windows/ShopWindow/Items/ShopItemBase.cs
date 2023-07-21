using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.ShopWindow
{
	public abstract class ShopItemBase : MonoBehaviour
	{
		protected bool IsBougth;
		protected bool IsEquipped;

		protected abstract void Buy();
		protected abstract void Equip();
	}
}