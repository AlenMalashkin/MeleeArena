using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.ShopWindow
{
	public abstract class ShopItemBase : MonoBehaviour
	{
		[SerializeField] private Button buyButton;

		private void Awake()
		{
			OnAwake();
		}

		protected void SubscribeOnBuy()
		{
			buyButton.onClick.AddListener(Buy);
		}

		protected void UnsubscribeOnBuy()
		{
			buyButton.onClick.RemoveListener(Buy);
		}
		
		protected abstract void OnAwake();
		protected abstract void Buy();
	}
}