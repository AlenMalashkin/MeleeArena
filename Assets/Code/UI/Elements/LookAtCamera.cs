using UnityEngine;

namespace Code.UI.Elements
{
	public class LookAtCamera : MonoBehaviour
	{
		private Camera _mainCamera;

		private void Start()
			=> _mainCamera = Camera.main;

		private void Update()
		{
			Quaternion rotation = _mainCamera.transform.rotation;
			transform.LookAt(transform.rotation * rotation * Vector3.back, rotation * Vector3.up);
		}
	}
}