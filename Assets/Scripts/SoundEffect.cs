using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SoundEffectTypes{  
	ButtonClick,
	CardFlip,
	RightPair,
	EndGame,
	NewRound,
	Wolf,
	Apple,
	Berry,
	Butterfly,
	Carrot,
	Cherry,
	Coin,
	Mushroom,
	Peach,
	Pineapple,
	Rabbit,
	Rock
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
		audioSource.volume = 10.0f;
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
			return Resources.Load("Sounds/CardSound/Wolf") as AudioClip;  
		case SoundEffectTypes.Apple:  
			return Resources.Load("Sounds/CardSound/Apple") as AudioClip;  
		case SoundEffectTypes.Berry:  
			return Resources.Load("Sounds/CardSound/Berry") as AudioClip;  
		case SoundEffectTypes.Butterfly:  
			return Resources.Load("Sounds/CardSound/Butterfly") as AudioClip;  
		case SoundEffectTypes.Carrot:  
			return Resources.Load("Sounds/CardSound/Carrot") as AudioClip;  
		case SoundEffectTypes.Cherry:  
			return Resources.Load("Sounds/CardSound/Cherry") as AudioClip;  
		case SoundEffectTypes.Coin:  
			return Resources.Load("Sounds/CardSound/Coin") as AudioClip;  
		case SoundEffectTypes.Mushroom:  
			return Resources.Load("Sounds/CardSound/Mushroom") as AudioClip;  
		case SoundEffectTypes.Peach:  
			return Resources.Load("Sounds/CardSound/Peach") as AudioClip;  
		case SoundEffectTypes.Pineapple:  
			return Resources.Load("Sounds/CardSound/Pineapple") as AudioClip;  
		case SoundEffectTypes.Rabbit:  
			return Resources.Load("Sounds/CardSound/Rabbit") as AudioClip;  
		case SoundEffectTypes.Rock:  
			return Resources.Load("Sounds/CardSound/Rock") as AudioClip;  
		}  	
		return null;  
	}  
} 
