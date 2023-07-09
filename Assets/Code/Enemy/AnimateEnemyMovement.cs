using System;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Enemy
{
    public class AnimateEnemyMovement : MonoBehaviour
    {
        private const float MinVelocity = 0.1f;
        
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private EnemyAnimator animator;

        private void Update()
        {
            if (CheckEnemyMovement())
                animator.Move();
            else
                animator.StopMove();
        }

        private bool CheckEnemyMovement()
            => agent.velocity.sqrMagnitude >= MinVelocity;
    }
}