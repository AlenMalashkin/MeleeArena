using Code.UI.Windows.ShopWindow;
using UnityEngine;

namespace Code.StaticData
{
	[CreateAssetMenu(fileName = "Item", menuName = "Items", order = 4)]
	public class ItemStaticData : ScriptableObject
	{
		[SerializeField] private ItemType type;
		[SerializeField] private Sprite sprite;
		[SerializeField] private int cost;
		public ItemType Type => type;
		public Sprite Sprite => sprite;
		public int Cost => cost;
	}
}