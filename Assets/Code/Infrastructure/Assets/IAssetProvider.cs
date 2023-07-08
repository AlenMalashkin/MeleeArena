using System.Threading.Tasks;
using Code.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Infrastructure.Assets
{
	public interface IAssetProvider : IService
	{
		Task<GameObject> Instantiate(string address, Vector3 at);
		Task<GameObject> Instantiate(string address, Transform under);
		Task<GameObject> Instantiate(string address);
		Task<T> Load<T>(AssetReference assetReference) where T : class;
		Task<T> Load<T>(string address) where T : class;
		void CleanUp();
		void Initialize();
	}
}