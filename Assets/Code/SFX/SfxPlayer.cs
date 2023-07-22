using UnityEngine;

namespace Code.SFX
{
	public class SfxPlayer : MonoBehaviour
	{
		[SerializeField] private AudioSource source;

		public void PlaySfx(AudioClip clip)
		{
			source.PlayOneShot(clip);
		}
	}
}