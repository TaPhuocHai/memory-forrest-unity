using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SoundEffectTypes{  
	ButtonClick,
	CardFlip,
	RightPair,
	EndGame,
	NewRound,
	Wolf
}  

public class SoundEffects  
{
	private Dictionary<SoundEffectTypes, GameObject> _gameObjectSoundsDic;

	private static SoundEffects _instance;
	public static SoundEffects Instance 
	{
		get {
			if (SoundEffects._instance == null) {
				_instance = new SoundEffects ();
			}
			return _instance;
		}
	}

	public SoundEffects ()
	{
		_gameObjectSoundsDic = new Dictionary<SoundEffectTypes, GameObject> ();
	}

	public static void Play(SoundEffectTypes effectToPlay) 
	{  
		GameObject gameObjectSound = null;
		if (SoundEffects.Instance._gameObjectSoundsDic.ContainsKey(effectToPlay)) {
			gameObjectSound = SoundEffects.Instance._gameObjectSoundsDic[effectToPlay];
		}
		if (gameObjectSound == null) {
			gameObjectSound = new GameObject("Sound Effect Object");  
			gameObjectSound.AddComponent<AudioSource>();  

			SoundEffects.Instance._gameObjectSoundsDic[effectToPlay] = gameObjectSound;
		}

		AudioSource audioSource = gameObjectSound.GetComponent<AudioSource> ();
			 
		audioSource.clip = GetSoundEffect(effectToPlay);  
		audioSource.volume = 1;
		if (PHSetting.IsSoundEffect) {
			audioSource.Play();  
		}
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
