using System;
using Code.Logic;
using UnityEngine;

namespace Code.Player
{
	public class PlayerAnimator : MonoBehaviour, IAnimationStateReader
	{
		public event Action<AnimatorState> StateEntered; 
		public event Action<AnimatorState> StateExited; 
		
		[SerializeField] private Animator animator;
		[SerializeField] private CharacterController controller;

		private static readonly int MoveHash = Animator.StringToHash("Move"); 
		private static readonly int AttackHash = Animator.StringToHash("Attack"); 
		private static readonly int HitHash = Animator.StringToHash("Hit"); 
		private static readonly int DieHash = Animator.StringToHash("Die");
		private static readonly int VictoryHash = Animator.StringToHash("Victory");

		private readonly int _idleStateHash = Animator.StringToHash("idle");
		private readonly int _attackStateHash = Animator.StringToHash("attack");
		private readonly int _runStateHash = Animator.StringToHash("run");
		private readonly int _dieStateHash = Animator.StringToHash("death");
		private readonly int _victoryStateHash = Animator.StringToHash("victory");
	
		public AnimatorState State { get; }
		public bool IsAttacking => State == AnimatorState.Attack;

		private void Update()
			=> animator.SetFloat(MoveHash, controller.velocity.magnitude, 0.1f, Time.deltaTime);

		public void PlayAttack() => animator.SetTrigger(AttackHash);
		public void PlayHit() => animator.SetTrigger(HitHash);
		public void PlayDeath() => animator.SetTrigger(DieHash);
		public void PlayVictory() => animator.SetTrigger(VictoryHash);

		public void EnteredState(int stateHash)
			=> StateEntered?.Invoke(ForState(stateHash));

		public void ExitedState(int stateHash)
			=> StateExited?.Invoke(ForState(stateHash));

		private AnimatorState ForState(int stateHash)
		{
			AnimatorState state;

			if (stateHash == _idleStateHash)
				state = AnimatorState.Idle;
			else if (stateHash == _runStateHash)
				state = AnimatorState.Move;
			else if (stateHash == _attackStateHash)
				state = AnimatorState.Attack;
			else if (stateHash == _dieStateHash)
				state = AnimatorState.Die;
			else if (stateHash == _victoryStateHash)
				state = AnimatorState.Victory;
			else
				state = AnimatorState.Unknown;

			return state;
		}
	}
}