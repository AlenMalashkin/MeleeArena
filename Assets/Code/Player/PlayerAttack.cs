using Code.Enemy;
using Code.Logic;
using Code.Services;
using Code.Services.Input;
using UnityEngine;

namespace Code.Player
{
	public class PlayerAttack : MonoBehaviour
	{
		[SerializeField] private PlayerAnimator animator;

		private int _damage;

		private PlayerSword _sword;
		private IInputService _inputService;
		private LayerMask _layerMask;
		private Collider[] _hits = new Collider[3];

		public void Construct(int damage, PlayerSword sword)
		{
			_damage = damage;
			_sword = sword;
		}
		
		private void Awake()
		{
			_layerMask = 1 << LayerMask.NameToLayer("Hittable");
			_inputService = ServiceLocator.Container.Resolve<IInputService>();
		}

		private void Update()
		{
			if (_inputService.IsAttackButtonUp() && !animator.IsAttacking)
				animator.PlayAttack();
		}

		private void OnAttack()
		{
			PhysicsDebug.DrawDebug(StartPos() + transform.forward, 0.75f, 1.0f);
			for (int i = 0; i < Hit(); i++)
			{
				if (_hits[i].transform.parent.TryGetComponent(out IHealth health))
				{
					health?.TakeDamage(_damage);
					_sword.PlaySwordParticles();
					_sword.PlaySwordHitSound();
				}
			}
		}

		private int Hit()
			=> Physics.OverlapSphereNonAlloc(StartPos() + transform.forward, 0.75f, _hits, _layerMask);

		private Vector3 StartPos()
			=> new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
	}
}