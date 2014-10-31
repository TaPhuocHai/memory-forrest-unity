using UnityEngine;
using System.Collections;
using Soomla;
using Soomla.Store;

public class ShopPopup : PHPopup 
{
	#region Singleton
	public static ShopPopup Instance { get; private set;}
	#endregion

	public PHPanel panel;

	public PHButton adsButton;
	public PHButton coinButton;

	void Awake () 
	{
		ShopPopup.Instance = this;		
		this.Init ();
	}

	void Start ()
	{
		if (this.adsButton) {
			this.adsButton.onClickHandle += HandleAdsButtonClick;
		}
		if (this.coinButton) {
			this.coinButton.onClickHandle += HandleCoinButtonClick;
		}

		// Prepare store 
		SoomlaStore.Initialize(new IAPAssets());

		// Add event
		/*
		StoreEvents.OnMarketPurchaseStarted      += OnMarketPurchaseStarted;
		StoreEvents.OnMarketPurchase             += OnMarketPurchase;
		StoreEvents.OnItemPurchaseStarted        += OnItemPurchaseStarted;
		StoreEvents.OnItemPurchased              += OnItemPurchased;
		StoreEvents.OnUnexpectedErrorInStore     += OnUnexpectedErrorInStore;
		*/
	}
	
	void Update () {}
	
	#region Animation
	
	override public void Hide (float second) 
	{
		base.Hide (second);
		this.panel.HideToDirection (PHPanelDirection.Top,second);
	}
	
	override public void Show (float second) 
	{
		base.Show (second);
		// Show panel
		this.panel.Show (second);
	}
	
	#endregion

	void HandleAdsButtonClick ()
	{
		StoreInventory.BuyItem (IAPAssets.NO_ADDS_NONCONS.ItemId);
	}

	void HandleCoinButtonClick ()
	{
		StoreInventory.BuyItem (IAPAssets.DOUBLE_COIN_CONS.ItemId);
	}

	#region Store Event

	public void OnMarketPurchaseStarted( PurchasableVirtualItem pvi ) {
		Debug.Log( "OnMarketPurchaseStarted: " + pvi.ItemId );
	}
	
	public void OnMarketPurchase( PurchasableVirtualItem pvi ) {
		Debug.Log( "OnMarketPurchase: " + pvi.ItemId );
	}
	
	public void OnItemPurchaseStarted( PurchasableVirtualItem pvi ) {
		Debug.Log( "OnItemPurchaseStarted: " + pvi.ItemId );
	}
	
	public void OnItemPurchased( PurchasableVirtualItem pvi ) {
		Debug.Log( "OnItemPurchased: " + pvi.ItemId );
	}

	public void OnUnexpectedErrorInStore( string err ) {
		Debug.Log( "OnUnexpectedErrorInStore" + err );
	}

	#endregion
}
