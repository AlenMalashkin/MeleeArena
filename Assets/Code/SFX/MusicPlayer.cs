using UnityEngine;

namespace Code.SFX
{
	public class MusicPlayer : MonoBehaviour
	{
		[SerializeField] private AudioSource source;

		public void PlayMusic(AudioClip clip)
		{
			source.clip = clip;
			source.PlayDelayed(1);
			source.loop = true;
		}
	}
}