using Code.Infrastructure.GameStates;
using Code.Services;
using Code.UI.LoadingCurtain;

namespace Code.Infrastructure
{
	public class Game
	{
		public GameStateMachine GameStateMachine { get; }

		public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
		{
			GameStateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, ServiceLocator.Container);
		}
	}
}