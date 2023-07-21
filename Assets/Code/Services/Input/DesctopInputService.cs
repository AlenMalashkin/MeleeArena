using UnityEngine;

namespace Code.Services.Input
{
	public class DesctopInputService : InputService
	{
		private const string AttackButton = "Fire1";
		
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

		public override bool IsAttackButtonUp()
			=> UnityEngine.Input.GetButtonUp(AttackButton);

		private static Vector2 GetUnityAxis()
			=> new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));
	}
}