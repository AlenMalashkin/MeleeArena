using System;
using Code.UI.Services.Factory;
using Code.UI.Windows;

namespace Code.UI.Services.WindowService
{
	public class WindowService : IWindowService
	{
		private readonly IUIFactory _uiFactory;
		
		public WindowService(IUIFactory uiFactory)
		{
			_uiFactory = uiFactory;
		}
		
		public void Open(WindowType type)
		{
			switch (type)
			{
				case WindowType.LoseWindow :
					_uiFactory.CreateLoseScreen();
					break;
				case WindowType.WinWindow :
					_uiFactory.CreateWinScreen();
					break;
				case WindowType.Shop :
					_uiFactory.CreateShop();
					break;
				case WindowType.PauseWindow :
					_uiFactory.CreatePauseWindow();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(type), type, null);
			}
		}
	}
}