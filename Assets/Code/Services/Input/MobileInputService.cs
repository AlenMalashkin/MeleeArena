using UnityEngine;

namespace Code.Services.Input
{
	public class MobileInputService : InputService
	{
		private const string AttackButton = "Fire";
		
		public override Vector2 MovementAxis => GetMovementAxis();

		public override bool IsAttackButtonUp()
			=> SimpleInput.GetButtonUp(AttackButton);
	}
}