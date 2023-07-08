using Code.UI.LoadingCurtain;
using Code.UI.Services.Factory;

namespace Code.Infrastructure.GameStates
{
	public class MenuState : IState
	{
		private SceneLoader _sceneLoader;
		private LoadingCurtain _loadingCurtain;
		private IUIFactory _uiFactory;
		
		public MenuState(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IUIFactory uiFactory)
		{
			_sceneLoader = sceneLoader;
			_loadingCurtain = loadingCurtain;
			_uiFactory = uiFactory;
		}
		
		public void Enter()
		{
			_loadingCurtain.Show();
			_sceneLoader.Load("Menu", OnLoad);
		}

		public void Exit()
		{
			
		}

		private void OnLoad()
		{
			InitMenu();
		}

		private async void InitMenu()
		{
			await _uiFactory.CreateUIRoot();
			_uiFactory.CreateMenu();
			_loadingCurtain.Hide();
		}
	}
}