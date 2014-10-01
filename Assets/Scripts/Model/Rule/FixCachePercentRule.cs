using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Fix cache percent rule.
/// Ke thua tu PercentRule nen co cac tinh chat giong nhu PercentRule
/// Khac voi PercentRule o isCache
/// Neu isCache = true : no se trar ve cardType da duoc tinh toan truoc do
/// </summary>
public class FixCachePercentRule : PercentRule
{
	protected CardType _cardTypeDidRandom;
	protected bool     _isSetValue;

	#region Contructors

	public FixCachePercentRule () 
		: base ()
	{
		this._isSetValue = false;
	}
	
	public FixCachePercentRule (Dictionary<CardType,float> cardTypeAndValue)
		: base (cardTypeAndValue)
	{
		this._isSetValue = false;
	}

	#endregion

	#region Override 

	override public string name { 
		get {
			return base.name;
		}
	}

	/// <summary>
	/// isCache = true : su dung ket qua cardType da random truoc do
	/// </summary>
	/// <value><c>true</c> if is cache; otherwise, <c>false</c>.</value>
	override public bool isCache { 
		get {return _isCache; }
		set {
			base.isCache = value;

			if (_isCache == false) {
				_isSetValue = false;
			}
		}
	}
	
	public override CardType RandomCard () 
	{
		if (_isSetValue == false || this.isCache == false) {
			_cardTypeDidRandom = base.RandomCard ();
			_isSetValue = true;
		}
		return _cardTypeDidRandom;
	}
	
	#endregion
}
