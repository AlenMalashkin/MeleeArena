using UnityEngine;

namespace Code.StaticData
{
	[CreateAssetMenu(fileName = "Item", menuName = "Items", order = 4)]
	public class ItemStaticData : ScriptableObject
	{
		public ItemData[] ItemDatas;
	}
}