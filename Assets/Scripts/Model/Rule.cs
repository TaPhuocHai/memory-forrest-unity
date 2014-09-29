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
	private List<int>  listValue;
	private List<Rule> listRule;

	public ComplexRule () 
	{
		listValue = new List<int> ();
		listRule = new List<Rule> ();
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ComplexRule"/> class.
	/// </summary>
	/// <param name="cardType">Card type.</param>
	/// <param name="value">Value : 0 -> 100</param>
	public ComplexRule (CardType cardType, int value)
	{
		listValue.Add (value);
		listRule.Add (new SimpleRule (cardType));
	}
	
	public ComplexRule (Dictionary<CardType,int> cardTypeAndValue)
	{
		foreach (CardType type in cardTypeAndValue.Keys) {
			int value = cardTypeAndValue[type];
			this.listValue.Add (value);
			this.listRule.Add (new SimpleRule (type));
		}
	}
	
	public void AddMore (CardType cardType, int value) 
	{
		this.listValue.Add (value);
		this.listRule.Add (new SimpleRule (cardType));
	}
	
	public void AddMore (Rule rule, int value) 
	{
		this.listValue.Add (value);
		this.listRule.Add (rule);
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
			foreach (Rule rule in this.listRule) {
				if (rule.isEnable) {
					return true;
				}
			}
			return false;
		}
	}

	public override CardType RandomCard () 
	{
		// Co mot so card chua duoc unlock
		// Can kiem tra va tao danh sach rule moi
		List<int>  listValueUnlocked;
		List<Rule> listRuleUnlocked;


		int radomValue = Random.Range (0, 100);

		int index = 0;
		int value = this.listValue[0];
		while (value < radomValue){
			index ++;
			if (index >= this.listValue.Count) {
				break;
			}
			value += this.listValue[index];
		}

		index --;
		Rule rule = this.listRule [index];
		return rule.RandomCard();
	}

	#endregion
}