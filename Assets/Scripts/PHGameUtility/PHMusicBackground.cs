using UnityEngine;
using System.Collections;

public class PHMusicBackground : MonoBehaviour 
{
	public bool isAutoPlay = true;

	#region Singleton
	public static PHMusicBackground Instance { get; private set;}
	#endregion

	public PHMusicBackground ()
	{
		PHMusicBackground.Instance = this;
	}

	void Start ()
	{
		if (this.isAutoPlay) {
			this.Play ();
		}
	}

	void Play ()
	{
		if (audio != null && !audio.isPlaying) {
			audio.Play();
		}
	}

	void Pause ()
	{
		if (audio != null && audio.isPlaying) {
			audio.Pause();
		}
	}

	void Stop ()
	{
		if (audio != null && audio.isPlaying) {
			audio.Stop();
		}
	}
}
