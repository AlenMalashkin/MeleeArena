using System;
using Code.Logic;
using UnityEngine;

namespace Code.Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
    {
        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;
        
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Die = Animator.StringToHash("Die");

        private readonly int _attackStateHash = Animator.StringToHash("attack");
        private readonly int _moveStateHash = Animator.StringToHash("move");
        private readonly int _hitStateHash = Animator.StringToHash("hit");
        private readonly int _deathStateHash = Animator.StringToHash("death");
        
        private Animator _animator;
        
        public AnimatorState State { get; }

        private void Awake()
            => _animator = GetComponent<Animator>();

        public void Move() => _animator.SetBool(IsMoving, true);
        public void StopMove() => _animator.SetBool(IsMoving, false);
        public void PlayAttack() => _animator.SetTrigger(Attack);
        public void PlayHit() => _animator.SetTrigger(Hit);
        public void PlayDie() => _animator.SetTrigger(Die);

        public void EnteredState(int stateHash)
            => StateEntered?.Invoke(ForState(stateHash));

        public void ExitedState(int stateHash)
            => StateExited?.Invoke(ForState(stateHash));
        
        private AnimatorState ForState(int stateHash)
        {
            AnimatorState state;

            if (stateHash == _moveStateHash)
                state = AnimatorState.Move;
            else if (stateHash == _attackStateHash)
                state = AnimatorState.Attack;
            else if (stateHash == _hitStateHash)
                state = AnimatorState.Hit;
            else if (stateHash == _deathStateHash)
                state = AnimatorState.Die;
            else
                state = AnimatorState.Unknown;

            Debug.Log(state);
            
            return state;
        }
    }
}