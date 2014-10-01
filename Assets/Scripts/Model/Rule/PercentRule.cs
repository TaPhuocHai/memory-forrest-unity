using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// PercentRule rule.
/// Chua danh sach Rule : ti le %
/// Moi lan goi RandomCard no se dua vao ti le % cua moi Rule de quyet dinh Rule nao duoc su dung de RandomCard
/// Vi moi loai card co the bi unlock, moi lan goi RandomCard no se tu dong chia ti le % cua rule bi lock sang cho cac rule khac
/// Card bi unlock duoc the hien duoi properties isEnable
/// Neu isCache = true : luu ket qua tinh toan : chia ti le % cua Rule bi unlock cho cac rule khac.
/// Neu isCache = false : xoa het du lieu tinh toan
/// </summary>
public class PercentRule : Rule
{
	private List<float>  _listValue;
	private List<Rule>   _listRule;
	
	private List<float>  _listValueEnable;
	private List<Rule>   _listRuleEnable;

	#region Contructor

	public PercentRule () 
	{
		_listValue = new List<float> ();
		_listRule = new List<Rule> ();
	}
	
	/// <summary>
	/// Initializes a new instance of the <see cref="ComplexRule"/> class.
	/// </summary>
	/// <param name="cardType">Card type.</param>
	/// <param name="value">Value : 0 -> 100</param>
	public PercentRule (CardType cardType, float value)
		: this ()
	{
		this._listValue.Add (value);
		this._listRule.Add (new SimpleRule (cardType));
	}
	
	public PercentRule (Dictionary<CardType,float> cardTypeAndValue)
		: this ()
	{
		foreach (CardType type in cardTypeAndValue.Keys) {
			float value = cardTypeAndValue[type];
			this._listValue.Add (value);
			this._listRule.Add (new SimpleRule (type));
		}
	}

	#endregion

	#region Functions
	
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

	#endregion
	
	#region Override 
	
	/// <summary>
	/// Rule nay duoc enable khi co it nhat 1 rule duoc enable
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
	
	/// <summary>
	/// The _is cache.
	/// Neu isCache = true : luu ket qua tinh toan (chia ti le % cua Rule bi unlock cho cac rule khac)
	/// cho lan goi RandomCard lan sau -> toi uu xu ly cho CPU
	/// Neu isCache = false : xoa het du lieu tinh toan
	/// </summary>
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
