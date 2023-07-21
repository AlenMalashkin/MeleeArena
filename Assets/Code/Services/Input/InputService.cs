using UnityEngine;

namespace Code.Services.Input
{
	public abstract class InputService : IInputService
	{
		protected const string Horizontal = "Horizontal";
		protected const string Vertical = "Vertical";

		public abstract Vector2 MovementAxis { get; }
		
		public abstract bool IsAttackButtonUp();

		protected static Vector2 GetMovementAxis()
			=> new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
	}
}

