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

	void Update ()
	{
		if (!PHSetting.IsSoundBackgroud && audio.isPlaying) {
			audio.Pause ();
		}
		if (PHSetting.IsSoundBackgroud && !audio.isPlaying) {
			audio.Play ();
		}
	}

	public void Play ()
	{
		if (!PHSetting.IsSoundBackgroud) {
			return;
		}
			
		if (audio != null && !audio.isPlaying) {
			audio.Play();
		}
	}

	public void Pause ()
	{
		if (audio != null && audio.isPlaying) {
			audio.Pause();
		}
	}

	public void Stop ()
	{
		if (audio != null && audio.isPlaying) {
			audio.Stop();
		}
	}
}
