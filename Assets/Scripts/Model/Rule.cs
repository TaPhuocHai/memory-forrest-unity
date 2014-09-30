﻿using UnityEngine;
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

	/// <summary>
	/// Randoms the card.
	/// </summary>
	/// <returns>The card.</returns>
	virtual public CardType RandomCard ()  { return CardType.Apple; }
}

public class SimpleRule : Rule 
{
	private CardType cardType;

	public SimpleRule (CardType cardType)
	{
		this.cardType = cardType;
	}

	#region Override 

	/// <summary>
	/// Rule dua tren cardtype, chi unlock rule neu card type duoc unlock
	/// </summary>
	/// <returns>true</returns>
	/// <c>false</c>
	/// 
	override public bool isEnable 
	{
		get {
			return Card.IsUnlock (cardType);
		}
	}

	override public  CardType RandomCard () 
	{
		return this.cardType;
	}

	#endregion
}

public class ComplexRule : Rule
{
	private List<float>  _listValue;
	private List<Rule>   _listRule;

	private List<float>  _listValueEnable;
	private List<Rule>   _listRuleEnable;

	public ComplexRule () 
	{
		_listValue = new List<float> ();
		_listRule = new List<Rule> ();
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ComplexRule"/> class.
	/// </summary>
	/// <param name="cardType">Card type.</param>
	/// <param name="value">Value : 0 -> 100</param>
	public ComplexRule (CardType cardType, float value)
		: this ()
	{
		this._listValue.Add (value);
		this._listRule.Add (new SimpleRule (cardType));
	}
	
	public ComplexRule (Dictionary<CardType,float> cardTypeAndValue)
		: this ()
	{
		foreach (CardType type in cardTypeAndValue.Keys) {
			float value = cardTypeAndValue[type];
			this._listValue.Add (value);
			this._listRule.Add (new SimpleRule (type));
		}
	}
	
	public void AddMore (CardType cardType, float value) 
	{
		this._listValue.Add (value);
		this._listRule.Add (new SimpleRule (cardType));
	}
	
	public void AddMore (Rule rule, float value) 
	{
		this._listValue.Add (value);
		this._listRule.Add (rule);
	}

	#region Override 

	/// <summary>
	/// Moi rule dua tren card type
	/// </summary>
	/// <returns>true</returns>
	/// <c>false</c>
	override public bool isEnable 
	{
		get {
			// Neu co 1 rule da duoc enable thi no duoc enable
			foreach (Rule rule in this._listRule) {
				if (rule.isEnable) {
					return true;
				}
			}
			return false;
		}
	}

	private bool _isCache;
	override public bool isCache { 
		get {return _isCache; }
		set {
			_isCache = value;
			if (_isCache == false) {
				_listRuleEnable = null;
				_listValueEnable = null;

				foreach (Rule rule in this._listRule) {
					rule.isCache = false;
				}
			} else {
				foreach (Rule rule in this._listRule) {
					rule.isCache = true;
				}
			}
		}
	}

	public override CardType RandomCard () 
	{
		// Co mot so card chua duoc unlock
		// Can kiem tra va tao danh sach rule moi
		if (_isCache == false || _listRuleEnable == null || _listRuleEnable.Count == 0) {
			// Khoi tao neu can thiet
			if (_listRuleEnable == null) {
				_listRuleEnable = new List<Rule> ();
				_listValueEnable = new List<float> ();
			}
			List<Rule> listRuleLocked = new List<Rule> ();
			List<float>  listValueLocked = new List<float> ();
			// Duyet qua cac phan tu 
			for (int i = 0 ; i < this._listRule.Count ; i ++) {
				Rule rule = this._listRule[i];
				// Neu rule nay chua enable thi luu lai thong tin
				if (rule.isEnable == false) {
					listRuleLocked.Add (rule);
					listValueLocked.Add (this._listValue[i]);
				}
				// Da enable rule nay
				else {
					this._listRuleEnable.Add (rule);
					this._listValueEnable.Add (this._listValue[i]);
				}
			}

			// Phan bo cac ti le % cua rule bi loced vao cac rule duoc enable
			foreach (float percent in listValueLocked) {
				float giaTriCongThem = percent/this._listValueEnable.Count;
				for (int i = 0 ; i < this._listValueEnable.Count ; i ++) {
					this._listValueEnable[i] = this._listValueEnable[i] + giaTriCongThem;
				}
			}
			for (int i = 0 ; i < this._listValueEnable.Count ; i ++) {
				Debug.Log ("ket qua : " + this._listValueEnable[i].ToString());
			}
		}

		// Random 1 so
		int radomValue = Random.Range (0, 100);

		int index = 0;
		float value = this._listValueEnable[0];
		// Tai phan tu i neu value > radomValue thi ket qua la i - 1;
		while (value < radomValue){
			index ++;
			if (index >= this._listValueEnable.Count) {
				index --;
				break;
			}
			value += this._listValueEnable[index];
		}

		Rule resultRule = this._listRuleEnable [index];
		return resultRule.RandomCard();
	}

	#endregion
}

public class CardRandomCode
{
	private Dictionary<CardType, int> _cardTypeAndNumberRequired;
	private Rule _rule;

	public CardRandomCode(Rule rule)
	{
		this._rule = rule;
	}

	public CardRandomCode(Rule rule, Dictionary<CardType, int> cardTypeAndNumberRequired)
	{
		this._rule = rule;
		this._cardTypeAndNumberRequired = cardTypeAndNumberRequired;
	}

	public Dictionary<CardType, int> GetCards (int totalCardNeed) 
	{
		// Bien chua thong tin card : so luong
		Dictionary<CardType, int> numberCardToRandomWithTypeKey = new Dictionary<CardType, int> ();

		// Get card can phai co
		foreach (CardType type in this._cardTypeAndNumberRequired.Keys) {
			numberCardToRandomWithTypeKey[type] = this._cardTypeAndNumberRequired[type];
		}

		// Dem so luong tong so card special da random
		int totalCarDidRandom = 0;
		foreach (CardType key in numberCardToRandomWithTypeKey.Keys) {
			totalCarDidRandom += (int)numberCardToRandomWithTypeKey[key];
		}

		this._rule.isEnable = true;

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

		this._rule.isEnable = false;

		return numberCardToRandomWithTypeKey;
	}
}