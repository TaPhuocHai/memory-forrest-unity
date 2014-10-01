using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Card random code.
/// Lop nay chua thong tin de GetCards cho moi round cua moi region
/// </summary>
public class CardRandomCode
{
	/// <summary>
	/// so luong cho moi cardtype yeu cau phai co khi GetCards
	/// </summary>
	private Dictionary<CardType, int> _cardTypeAndNumberRequired;
	/// <summary>
	/// Rule de random card
	/// </summary>
	private Rule _rule;

	#region Contructors

	public CardRandomCode(Rule rule)
	{
		this._rule = rule;
	}
	
	public CardRandomCode(Rule rule, Dictionary<CardType, int> cardTypeAndNumberRequired)
	{
		this._rule = rule;
		this._cardTypeAndNumberRequired = cardTypeAndNumberRequired;
	}

	#endregion
	
	public Dictionary<CardType, int> GetCards (int totalCardNeed) 
	{
		// Bien chua thong tin card : so luong
		Dictionary<CardType, int> numberCardToRandomWithTypeKey = new Dictionary<CardType, int> ();
		
		// Get card can phai co
		if (this._cardTypeAndNumberRequired != null && this._cardTypeAndNumberRequired.Count != 0) {
			foreach (CardType type in this._cardTypeAndNumberRequired.Keys) {
				numberCardToRandomWithTypeKey[type] = this._cardTypeAndNumberRequired[type];
			}
		}
		
		// Dem so luong tong so card special da random
		int totalCarDidRandom = 0;
		foreach (CardType key in numberCardToRandomWithTypeKey.Keys) {
			totalCarDidRandom += (int)numberCardToRandomWithTypeKey[key];
		}
		
		this._rule.isCache = true;
		
		// So luong card con lai danh cho card thuong
		int numberOfNormalCard = totalCardNeed - totalCarDidRandom;
		
		// Neu la so le thi bo sung them 1 card da
		if (numberOfNormalCard % 2 == 1) {
			int numberCardStone = 0;
			if (numberCardToRandomWithTypeKey.ContainsKey(CardType.Stone)) {
				numberCardStone = numberCardToRandomWithTypeKey[CardType.Stone];
			}
			numberCardStone += 1;
			numberCardToRandomWithTypeKey[CardType.Stone] = numberCardStone;
		}
		
		// Random cac cap thuong
		for (int i = 0; i < numberOfNormalCard/2; i ++) {		
			// Random type
			CardType type = this._rule.RandomCard ();
			
			// Lay so luong da random truoc danh cho type nay
			int numberCardDidRandomForType = 0;
			if (numberCardToRandomWithTypeKey.ContainsKey(type)) {
				numberCardDidRandomForType = numberCardToRandomWithTypeKey[type];
			}
			numberCardDidRandomForType += 2;
			numberCardToRandomWithTypeKey[type] = numberCardDidRandomForType;
		}
		
		this._rule.isCache = false;
		
		return numberCardToRandomWithTypeKey;
	}
}