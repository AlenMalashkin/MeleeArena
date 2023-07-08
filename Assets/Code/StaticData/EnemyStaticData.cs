using Code.Enemy;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.StaticData
{
	[CreateAssetMenu(menuName = "EnemyStaticData", fileName = "EnemyStaticData", order = 1)]
	public class EnemyStaticData : ScriptableObject
	{
		[SerializeField] private AssetReferenceGameObject assetReferenceGameObject;
		[SerializeField] private EnemyType type;
		[SerializeField] private int health;
		[SerializeField] private int damage;
		[SerializeField] private float attackCooldown;
		[SerializeField] private float attackDistance;
		[SerializeField] private float moveSpeed;
		public AssetReferenceGameObject AssetReferenceGameObject => assetReferenceGameObject;
		public EnemyType Type => type;
		public int Health => health;
		public int Damage => damage;
		public float AttackCooldown => attackCooldown;
		public float AttackDistance => attackDistance;
		public float MoveSpeed => moveSpeed;
	}
}

