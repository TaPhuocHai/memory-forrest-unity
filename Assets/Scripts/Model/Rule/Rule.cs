using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Rule.
/// </summary>
public abstract class Rule
{	
	virtual public bool isEnable 
	{
		get {
			return true;
		}
	}

	virtual public bool isCache { get ; set; }

	virtual public string name { get; private set;}

	/// <summary>
	/// Randoms the card.
	/// </summary>
	/// <returns>The card.</returns>
	virtual public CardType RandomCard ()  { return CardType.Apple; }
}