using UnityEngine;

namespace Code.Services.Input
{
	public class DesctopInputService : InputService
	{
		public override Vector2 MovementAxis
		{
			get
			{
				Vector2 axis = GetMovementAxis();

				if (axis == Vector2.zero)
				{
					axis = GetUnityAxis();
				}
				
				return axis;
			}
		}

		private static Vector2 GetUnityAxis()
			=> new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));
	}
}