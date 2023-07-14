using Code.Infrastructure.GameStates;
using Code.Services;
using Code.UI.Services.WindowService;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.MenuWindow
{
	public class MenuWindow : WindowBase
	{
		[SerializeField] private Button play;
		[SerializeField] private Button shop;

		private IGameStateMachine _gameStateMachine;
		private IWindowService _windowService;
		
		public void Construct(IGameStateMachine gameStateMachine)
		{
			_gameStateMachine = gameStateMachine;
		}
		
		protected override void OnAwake()
		{
			_windowService = ServiceLocator.Container.Resolve<IWindowService>();
			
			play.onClick.AddListener(() => _gameStateMachine.Enter<LoadLevelState, string>("Main"));
			shop.onClick.AddListener(() => _windowService.Open(WindowType.Shop));
		}
	}
}