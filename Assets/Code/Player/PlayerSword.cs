using Code.SFX;
using UnityEngine;

namespace Code.Player
{
	public class PlayerSword : MonoBehaviour
	{
		[SerializeField] private ParticleSystem swordParticles;
		[SerializeField] private SfxPlayer swordSfxPlayer;
		[SerializeField] private AudioClip hitClip;	 

		public void PlaySwordParticles()
		{
			swordParticles.Play();
		}

		public void PlaySwordHitSound()
		{
			swordSfxPlayer.PlaySfx(hitClip);
		}
	}
}