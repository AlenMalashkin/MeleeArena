using System.Collections.Generic;
using Code.Data;
using UnityEngine;

namespace Code.StaticData
{
	[CreateAssetMenu(fileName = "WindowStaticData", menuName = "WindowStaticData", order = 3)]
	public class WindowStaticData : ScriptableObject
	{
		public List<WindowConfig> Configs;
	}
}