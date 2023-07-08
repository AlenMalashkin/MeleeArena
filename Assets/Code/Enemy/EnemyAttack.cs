using System;
using System.Linq;
using Code.Logic;
using UnityEngine;

namespace Code.Enemy
{
	public class EnemyAttack : MonoBehaviour
	{
		private Transform _target;
		private Collider[] _hits  = new Collider[1];
		private LayerMask _layerMask;
		private int _damage;
		private float _attackCooldown;
		private float _attackCooldownTemp;
		private float _attackDistance;
		private bool _attackIsActive;
		private bool _isAttacking = false;

		private void Awake() => 
			_layerMask = 1 << LayerMask.NameToLayer("Player");
		
		private void Update()
		{
			UpdateCooldown();
			
			if (CanAttack())
				StartAttack();
		}

		public void Construct(Transform target, int damage, float attackCooldown, float attackDistance)
		{
			_target = target;
			_damage = damage;
			_attackCooldown = attackCooldown;
			_attackCooldownTemp = attackCooldown;
			_attackDistance = attackDistance;
		}

		public void EnableAttack()
			=> _attackIsActive = true;

		public void DisableAttack()
			=> _attackIsActive = false;

		private bool CooldownIsUp()
			=> _attackCooldownTemp <= 0;

		private void UpdateCooldown()
		{
			if (!CooldownIsUp())
				_attackCooldownTemp -= Time.deltaTime;
		}

		private bool Hit(out Collider hit)
		{
			var hitAmount = Physics.OverlapSphereNonAlloc(StartPosition(), 0.5f, _hits, _layerMask);

			hit = _hits.FirstOrDefault();

			return hitAmount > 0;
		}

		private Vector3 StartPosition()
		{
			return new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) +
				Vector3.forward * _attackDistance;
		}

		private bool CanAttack()
			=> _attackIsActive && !_isAttacking && CooldownIsUp();

		private void StartAttack()
		{
			transform.LookAt(_target.position);
			
			if (Hit(out Collider other))
				other.GetComponent<IHealth>().TakeDamage(_damage);
			
			_attackCooldownTemp = _attackCooldown;
		}
	}
}
