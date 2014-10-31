using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Simple rule : luon luon tra ve 1 loai card
/// </summary>
public class SimpleRule : Rule 
{
	private CardType cardType;
	
	public SimpleRule (CardType cardType)
	{
		this.cardType = cardType;
	}
	
	#region Override 
	
	/// <summary>
	/// Rule enable dua tren cardtype, chi unlock rule neu card type duoc unlock
	/// </summary>
	/// <returns>true</returns>
	/// <c>false</c>
	/// 
	override public bool isEnable 
	{
		get {
			return Card.IsUnlock (cardType,Player.currentRegionPlay);
		}
	}
	
	override public  CardType RandomCard () 
	{
		return this.cardType;
	}

	override public string name { get { return this.cardType.ToString(); }}

	#endregion
}