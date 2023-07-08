using UnityEngine;

namespace Code.CameraLogic
{
	public class FollowingCamera : MonoBehaviour
	{
		[SerializeField] private float rotationX = 55f;
		[SerializeField] private float distance = 10f;
		[SerializeField] private float offset = 0.5f;

		private Transform _target;
		
		private void LateUpdate()
		{
			if (_target == null)
				return;
			
			Quaternion rotation = Quaternion.Euler(rotationX, 0, 0);
			Vector3 position = rotation * new Vector3(0, 0, -distance) + FollowingObjectPosition();

			transform.rotation = rotation;
			transform.position = position;
		}

		private Vector3 FollowingObjectPosition()
		{
			Vector3 followingObjectPosition = _target.position;
			followingObjectPosition.y += offset;
			return followingObjectPosition;
		}

		public void Follow(GameObject target)
		{
			_target = target.transform;
		}
	}
}