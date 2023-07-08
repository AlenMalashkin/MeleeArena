using Code.Services;

namespace Code.Infrastructure.GameStates
{
	public interface IGameStateMachine : IService
	{
		void Enter<TState>() where TState : class, IState;
		void Enter<TPayloadState, TPayload>(TPayload payload) where TPayloadState : class, IPayloadedState<TPayload>;
	}
}