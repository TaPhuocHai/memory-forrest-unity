using UnityEngine;
using System.Collections;

public enum SoundEffectTypes{  
	ButtonClick,
	CardFlip,
	RightPair,
	EndGame,
	NewRound,
	Wolf
}  

public static class SoundEffects  
{
	public static void Play(SoundEffectTypes effectToPlay) {  
		GetAudioSource().clip = GetSoundEffect(effectToPlay);  
		if (Player.IsSoundEffect) {
			GetAudioSource().Play();  
		}
	}  

	static GameObject soundObjectInstance = null;  
	static AudioSource GetAudioSource() {  
		//if it's null, build the object  
		if(soundObjectInstance == null){  
			soundObjectInstance = new GameObject("Sound Effect Object");  
			soundObjectInstance.AddComponent<AudioSource>();  
		}  
		return soundObjectInstance.GetComponent<AudioSource>();  
	}  

	public static void SetVolume(float newVolume){  
		GetAudioSource().volume = newVolume;  
	}  

	public static float GetCurrentVolume(){  
		return GetAudioSource().volume;  
	}  

	static AudioClip GetSoundEffect(SoundEffectTypes effectToPlay){  
		switch(effectToPlay){  
		case SoundEffectTypes.ButtonClick:  
			return Resources.Load("Sounds/ButtonClick") as AudioClip;  
		case SoundEffectTypes.CardFlip:  
			return Resources.Load("Sounds/CardFlip") as AudioClip;  
		case SoundEffectTypes.RightPair:  
			return Resources.Load("Sounds/RightPair") as AudioClip;  
		case SoundEffectTypes.EndGame:  
			return Resources.Load("Sounds/EndGame") as AudioClip;  
		case SoundEffectTypes.NewRound:  
			return Resources.Load("Sounds/NewRound") as AudioClip;  
		case SoundEffectTypes.Wolf:  
			return Resources.Load("Sounds/Wolf") as AudioClip;  
		}  
		return null;  
	}  
} 
