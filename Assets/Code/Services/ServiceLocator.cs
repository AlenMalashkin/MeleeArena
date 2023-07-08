namespace Code.Services
{
	public class ServiceLocator
	{
		private static ServiceLocator _instance;
		public static ServiceLocator Container => _instance ??= new ServiceLocator();

		public void RegisterService<TService>(TService instance) where TService : IService
			=> InstanceHolder<TService>.ServiceInstance = instance;
		
		public TService Resolve<TService>() where TService : IService
			=> InstanceHolder<TService>.ServiceInstance;

		private class InstanceHolder<TService> where TService : IService
		{
			public static TService ServiceInstance;
		}
	}
}