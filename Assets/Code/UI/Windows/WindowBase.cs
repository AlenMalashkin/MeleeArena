using UnityEngine;

namespace Code.UI.Windows
{
	public abstract class WindowBase : MonoBehaviour
	{
		private void Awake()
		{
			OnAwake();
		}

		protected abstract void OnAwake();
	}
}