using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Code.Infrastructure.Assets
{
	public class AssetProvider : IAssetProvider
	{
		private readonly Dictionary<string, AsyncOperationHandle> _completedTasks = new Dictionary<string, AsyncOperationHandle>();
		private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new Dictionary<string, List<AsyncOperationHandle>>();
		
		public void Initialize()
		{
			Addressables.InitializeAsync();
		}

		public async Task<T> Load<T>(AssetReference assetReference) where T : class
		{
			if (_completedTasks.TryGetValue(assetReference.AssetGUID, out AsyncOperationHandle handle))
				return handle.Result as T;

			return await ReturnCompletedHandle(Addressables.LoadAssetAsync<T>(assetReference), assetReference.AssetGUID);
		}

		public async Task<T> Load<T>(string address) where T : class
		{
			if (_completedTasks.TryGetValue(address, out AsyncOperationHandle handle))
				return handle.Result as T;

			return await ReturnCompletedHandle(Addressables.LoadAssetAsync<T>(address), address);
		}

		public Task<GameObject> Instantiate(string address, Vector3 at)
			=> Addressables.InstantiateAsync(address, at, Quaternion.identity).Task;

		public Task<GameObject> Instantiate(string address, Transform under)
			=> Addressables.InstantiateAsync(address, under).Task;

		public Task<GameObject> Instantiate(string address)
			=> Addressables.InstantiateAsync(address).Task;
		
		
		public void CleanUp()
		{
			foreach (List<AsyncOperationHandle> handles in _handles.Values)
			{
				foreach (var handle in handles)
				{
					Addressables.Release(handle);
				}
			}

			_completedTasks.Clear();
			_handles.Clear();
		}

		private async Task<T> ReturnCompletedHandle<T>(AsyncOperationHandle<T> handle, string key) where T : class
		{
			handle.Completed += handleCompleted 
				=> _completedTasks[key] = handleCompleted;

			AddHandle<T>(key, handle);
			
			return await handle.Task;
		}

		private void AddHandle<T>(string key, AsyncOperationHandle handle) where T : class
		{
			if (!_handles.TryGetValue(key, out List<AsyncOperationHandle> handles))
			{
				handles = new List<AsyncOperationHandle>();
				_handles[key] = handles;
			}
			
			handles.Add(handle);
		}
	}
}