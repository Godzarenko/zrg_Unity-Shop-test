using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Shop/Shop offer")]
public class ShopItemSO : ScriptableObject
{
    public string ShopItemUID;
    public string PlayStoreUID;

    public string OfferName;
    [Multiline]
    public string OfferDescription;

    public List<ItemCraftEntryStruct> Items;

    public ItemSO ResultItem;

    public float DebugPrice;
    [Range(0f,1f)]
    public float DebugDiscount;

    public float GetPrice()
    {
#if UNITY_EDITOR
        return DebugPrice;
# elif UNITY_ANDROID
        //android IAP Manager
        var Product = GetProductById(PlayStoreUID);
        return Product.metadata.localizedPrice;
#endif
    }
    public float GetDiscountedPrice()
    {
#if UNITY_EDITOR
        return DebugPrice * (1 - DebugDiscount);
# elif UNITY_ANDROID
        //android IAP Manager
        var Product = GetProductById(PlayStoreUID);
        return ;
#endif
    }
    public float GetDiscount()
    {
#if UNITY_EDITOR
        return DebugDiscount;
# elif UNITY_ANDROID
        //android IAP Manager
        var Product = GetProductById(PlayStoreUID);
        return ;
#endif
    }
    public string GetPriceString()
    {
#if UNITY_EDITOR
        return $"{GetPrice()}$";
# elif UNITY_ANDROID
        //android IAP Manager
        var Product = GetProductById(PlayStoreUID);
        return Product.metadata.localizedPrice  + Product.metadata.isoCurrencyCode;
#endif
    }
    public string GetDiscountedPriceString()
    {
#if UNITY_EDITOR
        return $"{GetDiscountedPrice()}$";
# elif UNITY_ANDROID
        //android IAP Manager
        var Product = GetProductById(PlayStoreUID);
        return  + Product.metadata.isoCurrencyCode;
#endif
    }
    public string GetDiscountString()
    {
#if UNITY_EDITOR
        return $"-{Mathf.FloorToInt(DebugDiscount*100)}%";
# elif UNITY_ANDROID
        //android IAP Manager
        var Product = GetProductById(PlayStoreUID);
        return  + "%";
#endif
    }
    [System.Serializable]
    public struct ItemCraftEntryStruct
    {
        public ItemSO Item;
        public int Count;
    }
}

public interface IShopItemView
{
    public void SetShopItem(string ShopItemUID, string lable, string desc, string resultItem, string[] ItemsIn, int[] Counts, string LocalizedPrice, string LocalizedDiscountedPrice, string Discount);
    public void SetShopItem(ShopItemSO Item);
}

[System.Serializable]
public class SerializedShopItemSO : ISerializationCallbackReceiver
{
    public bool IsAvaliable;
    public ShopItemSO SerializedItem
    {
        get
        {
            if (serializedItem == null)
            {
                Parse();
            }
            return serializedItem;
        }
        set
        {
            serializedItem = value;
            BeforeSerialize();
        }
    }
    [System.NonSerialized] ShopItemSO serializedItem;

    [SerializeField] string UID;
    [SerializeField] string PlayStoreUID;
    [SerializeField] string Lable;
    [SerializeField] string Desc;
    [SerializeField] string ResultItemUID;
    [SerializeField] List<string> ItemsInUIDs;
    [SerializeField] List<int> ItemsInCounts;
    [SerializeField] float DebugPrice;
    [SerializeField] float DebugDiscount;

    void Parse()
    {
        ShopItemSO NSI = new ShopItemSO();
        NSI.ShopItemUID = UID;
        NSI.PlayStoreUID = PlayStoreUID;
        NSI.OfferName = Lable;
        NSI.OfferDescription = Desc;
        NSI.ResultItem = ItemsManager.Instance.GetItem(ResultItemUID);
        NSI.DebugPrice = DebugPrice;
        NSI.DebugDiscount = DebugDiscount;
        for (int i = 0; i < ItemsInUIDs.Count; i++)
        {
            if (ItemsInCounts[i] <= 0)
            {
                throw new ArgumentOutOfRangeException("Item count must be > 0");
            }
            ShopItemSO.ItemCraftEntryStruct ICES = new ShopItemSO.ItemCraftEntryStruct();
            ICES.Item = ItemsManager.Instance.GetItem(ItemsInUIDs[i]);
            ICES.Count = ItemsInCounts[i];
        }
        serializedItem = NSI;
    }
    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        Parse();
    }
    void BeforeSerialize()
    {
        if (SerializedItem != null)
        {
            UID = SerializedItem.ShopItemUID;
            PlayStoreUID = SerializedItem.PlayStoreUID;
            Lable = SerializedItem.OfferName;
            Desc = SerializedItem.OfferDescription;
            ResultItemUID = SerializedItem.ResultItem.ItemUID;
            ItemsInUIDs = new List<string>();
            ItemsInCounts = new List<int>();
            for (int i = 0; i < SerializedItem.Items.Count; i++)
            {
                ItemsInUIDs.Add(SerializedItem.Items[i].Item.ItemUID);
                ItemsInCounts.Add(SerializedItem.Items[i].Count);
            }
            DebugPrice = SerializedItem.DebugPrice;
            DebugDiscount = SerializedItem.DebugDiscount;
        }
    }
    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
        BeforeSerialize();
    }
}