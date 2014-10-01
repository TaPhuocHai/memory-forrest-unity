using UnityEngine;
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
	protected CardType cardTypeDidRandom;

	#region Override 
	
	/// <summary>
	/// isCache = true : su dung ket qua cardType da random truoc do
	/// </summary>
	/// <value><c>true</c> if is cache; otherwise, <c>false</c>.</value>
	override public bool isCache { 
		get {return _isCache; }
		set {
			base.isCache = value;

			if (_isCache == false) {
				cardTypeDidRandom = null;
			}
		}
	}
	
	public override CardType RandomCard () 
	{
		if (cardTypeDidRandom == null || !this.isCache) {
			cardTypeDidRandom = base.RandomCard ();
		}
		return cardTypeDidRandom;
	}
	
	#endregion
}
