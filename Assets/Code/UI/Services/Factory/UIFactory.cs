using System.Threading.Tasks;
using Code.Data;
using Code.Infrastructure;
using Code.Infrastructure.Assets;
using Code.Infrastructure.GameStates;
using Code.Services.StaticData;
using Code.UI.Windows;
using Code.UI.Windows.LoseWindow;
using Code.UI.Windows.MenuWindow;
using UnityEngine;

namespace Code.UI.Services.Factory
{
	public class UIFactory : IUIFactory
	{
		private IGameStateMachine _gameStateMachine;
		private IAssetProvider _assetProvider;
		private IStaticDataService _staticDataService;
		private LoadingCurtain.LoadingCurtain _loadingCurtain;
		
		private Transform _uiRoot;
		
		public UIFactory(IGameStateMachine gameStateMachine, IAssetProvider assetProvider, IStaticDataService staticDataService, LoadingCurtain.LoadingCurtain loadingCurtain)
		{
			_gameStateMachine = gameStateMachine;
			_assetProvider = assetProvider;
			_staticDataService = staticDataService;
			_loadingCurtain = loadingCurtain;
		}
		
		public void CreateLoseScreen()
		{
			WindowConfig config = _staticDataService.ForWindow(WindowType.LoseWindow);
			LoseWindow loseWindow = Object.Instantiate(config.WindowBase, _uiRoot) as LoseWindow;
			loseWindow.Construct(_gameStateMachine, _loadingCurtain);
		}

		public void CreateMenu()
		{
			WindowConfig config = _staticDataService.ForWindow(WindowType.MenuWindow);
			MenuWindow menuWindow = Object.Instantiate(config.WindowBase, _uiRoot) as MenuWindow;
			menuWindow.Construct(_gameStateMachine);
		}

		public async Task CreateUIRoot()
		{
			GameObject root = await _assetProvider.Instantiate("UIRoot");
			_uiRoot = root.transform;
		}
	}
}