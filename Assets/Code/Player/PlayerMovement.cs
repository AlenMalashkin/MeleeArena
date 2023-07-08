using Code.Services;
using Code.Services.Input;
using UnityEngine;

namespace Code.Player
{
	public class PlayerMovement : MonoBehaviour
	{
		[SerializeField] private CharacterController controller;
		[SerializeField] private float moveSpeed;

		private IInputService _inputService;
		private Camera _camera;

		private void Awake()
		{
			_inputService = ServiceLocator.Container.Resolve<IInputService>();
		}

		private void Start()
		{
			_camera = Camera.main;
		}

		private void Update()
		{
			Vector3 movementVector = Vector3.zero;

			if (_inputService.MovementAxis.sqrMagnitude > Mathf.Epsilon)
			{
				movementVector = _camera.transform.TransformDirection(_inputService.MovementAxis);
				movementVector.y = 0;
				movementVector.Normalize();

				transform.forward = movementVector;
			}
			
			movementVector += Physics.gravity;

			controller.Move(movementVector * moveSpeed * Time.deltaTime);
		}
	}
}
