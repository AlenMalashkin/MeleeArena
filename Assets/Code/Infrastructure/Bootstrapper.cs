using Code.Infrastructure.GameStates;
using Code.UI.LoadingCurtain;
using UnityEngine;

namespace Code.Infrastructure
{
	public class Bootstrapper : MonoBehaviour, ICoroutineRunner
	{
		[SerializeField] private LoadingCurtain curtainPrefab;
		private Game _game;

		private void Awake()
		{
			_game = new Game(this, Instantiate(curtainPrefab));
			_game.GameStateMachine.Enter<BootstrapState>();
			
			DontDestroyOnLoad(this);
		}
	}
}
