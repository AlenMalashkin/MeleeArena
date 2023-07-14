using System;
using Code.Extensions.DataExtensions;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Enemy
{
	public class EnemyMovement : MonoBehaviour
	{
		[SerializeField] private NavMeshAgent agent;

		private const float MinDistance = 1f;
		
		private float _moveSpeed;
		private Transform _playerPosition;
		
		public void Construct(float moveSpeed, Transform playerPosition)
		{
			_moveSpeed = moveSpeed;
			_playerPosition = playerPosition;
		}

		private void Start()
			=> agent.speed = _moveSpeed;

		private void Update()
		{
			if (TargetIsNotReached())
				agent.destination = _playerPosition.position;
		}

		private bool TargetIsNotReached()
			=> agent.transform.position.SqrMagnitudeToTarget(_playerPosition.position) >= MinDistance;
	}
}