using UnityEngine;
using System.Collections;
using Soomla;
using Soomla.Store;

public class IAPAssets : IStoreAssets 
{	
	public int GetVersion() {
		return 0;
	}
	
	public VirtualCurrency[] GetCurrencies() {
		return new VirtualCurrency[]{};
	}
	
	public VirtualGood[] GetGoods() {
		return new VirtualGood[] {};
	}
	
	public VirtualCurrencyPack[] GetCurrencyPacks() {
		return new VirtualCurrencyPack[] {};
	}
	
	public VirtualCategory[] GetCategories() {
		return new VirtualCategory[]{};
	}
	
	public VirtualCurrencyPack[] GetNonConsumableItems() {
		return new VirtualCurrencyPack[]{NO_ADDS_NONCONS, DOUBLE_COIN_CONS};//add Names
	}
	
	/** Static Final members **/

	public const string NO_ADDS_NONCONS_PRODUCT_ID  = "me.ice.memory.ads";
	public const string DOUBLE_COIN_PRODUCT_ID      = "me.ice.memory.coindoubler";
	
	
	/** Market MANAGED Items **/

	public static VirtualCurrencyPack NO_ADDS_NONCONS  = new VirtualCurrencyPack(
		"No Ads",//Name
		"Test purchase of MANAGED item.",//Description
		"no_ads",//id
		0,
		NO_ADDS_NONCONS_PRODUCT_ID,
		new PurchaseWithMarket(new MarketItem(NO_ADDS_NONCONS_PRODUCT_ID, MarketItem.Consumable.NONCONSUMABLE , 0.99))
		);
	public static VirtualCurrencyPack DOUBLE_COIN_CONS  = new VirtualCurrencyPack(
		"Green Colour",
		"Test purchase of MANAGED item.",
		"Green",
		10,
		DOUBLE_COIN_PRODUCT_ID,
		new PurchaseWithMarket(new MarketItem(DOUBLE_COIN_PRODUCT_ID, MarketItem.Consumable.CONSUMABLE , 0.99))
		);
}
