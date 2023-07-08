using UnityEngine;

namespace Code.Infrastructure
{
	public class GameRunner : MonoBehaviour
	{
		[SerializeField] private Bootstrapper bootstrapperPrefab;

		private void Awake()
		{
			Bootstrapper bootstrapper = FindObjectOfType<Bootstrapper>();

			if (bootstrapper != null)
				return;

			Instantiate(bootstrapperPrefab);
		}
	}
}