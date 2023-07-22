using System.Threading.Tasks;
using Code.Data;
using Code.Infrastructure.Assets;
using Code.Infrastructure.GameStates;
using Code.Services.StaticData;
using Code.UI.Windows;
using Code.UI.Windows.LoseWindow;
using Code.UI.Windows.MenuWindow;
using Code.UI.Windows.PauseWindow;
using Code.UI.Windows.WinWindow;
using UnityEngine;

namespace Code.UI.Services.Factory
{
	public class UIFactory : IUIFactory
	{
		private IGameStateMachine _gameStateMachine;
		private IAssetProvider _assetProvider;
		private IStaticDataService _staticDataService;
		
		private Transform _uiRoot;
		
		public UIFactory(IGameStateMachine gameStateMachine, IAssetProvider assetProvider, 
			IStaticDataService staticDataService)
		{
			_gameStateMachine = gameStateMachine;
			_assetProvider = assetProvider;
			_staticDataService = staticDataService;
		}
		
		public void CreateLoseScreen()
		{
			WindowConfig config = _staticDataService.ForWindow(WindowType.LoseWindow);
			LoseWindow loseWindow = Object.Instantiate(config.WindowBase, _uiRoot) as LoseWindow;
			loseWindow.Construct(_gameStateMachine);
		}

		public void CreateWinScreen()
		{
			WindowConfig config = _staticDataService.ForWindow(WindowType.WinWindow);
			WinWindow winWindow = Object.Instantiate(config.WindowBase, _uiRoot) as WinWindow;
			winWindow.Construct(_gameStateMachine);
		}

		public void CreateMenu()
		{
			WindowConfig config = _staticDataService.ForWindow(WindowType.MenuWindow);
			MenuWindow menuWindow = Object.Instantiate(config.WindowBase, _uiRoot) as MenuWindow;
			menuWindow.Construct(_gameStateMachine);
		}

		public void CreateShop()
		{
			WindowConfig config = _staticDataService.ForWindow(WindowType.Shop);
			Object.Instantiate(config.WindowBase, _uiRoot);
		}

		public void CreatePauseWindow()
		{
			WindowConfig config = _staticDataService.ForWindow(WindowType.PauseWindow);
			PauseWindow pauseWindow = Object.Instantiate(config.WindowBase, _uiRoot) as PauseWindow;
			pauseWindow.Construct(_gameStateMachine);
		}

		public async Task CreateUIRoot()
		{
			GameObject root = await _assetProvider.Instantiate("UIRoot");
			_uiRoot = root.transform;
		}
	}
}