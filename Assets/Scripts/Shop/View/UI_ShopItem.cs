using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ShopItem : MonoBehaviour, IShopItemView
{
    public UI_ShopWindow parentWindow;
    ShopItemSO ShopItem;
    public Transform ItemsRoot;
    List<GameObject> CurrentItems = new List<GameObject>();
    public TMPro.TextMeshProUGUI Lable;
    public TMPro.TextMeshProUGUI Description;
    public UnityEngine.UI.Image OfferPromoIcon;

    public UI_ShopBuyButton BuyButton;

    static readonly int MaximumItemsInOffer = 6;

    public void SetShopItem(ShopItemSO I)
    {
        if (I == null)
        {
            throw new NullReferenceException("Shop item cant be NULL");
        }
        if(ShopItem != I)
        {
            Lable.text = I.OfferName;
            Description.text = I.OfferDescription;
            OfferPromoIcon.sprite = I.ResultItem.ItemPromoIcon;

            BuyButton.SetData(I.ShopItemUID, I.GetPriceString(), I.GetDiscountedPriceString(), I.GetDiscountString());

            for(int i = 0; i < CurrentItems.Count; i++)
            {
                Destroy(gameObject);
            }
            CurrentItems.Clear();
            int mx = Mathf.Min(I.Items.Count, MaximumItemsInOffer);
            if(mx != I.Items.Count)
            {
                Debug.LogWarning($"Offer contains more items, then view supports ({I.Items.Count} in offer vs {MaximumItemsInOffer} maximum)");
            }
            for(int i = 0; i < mx; i++) {
                GameObject NGO = parentWindow.CreateItemIcon(I.Items[i].Item, I.Items[i].Count).gameObject;
                NGO.transform.parent = ItemsRoot;
            }

            BuyButton.SetData(I.ShopItemUID, I.GetPriceString(), I.GetDiscountedPriceString(), I.GetDiscountString());
        }
    }
    public void SetShopItem(string ShopItemUID, string lable, string desc, string resultItem, string[] ItemsIn, int[] Counts, string LocalizedPrice, string LocalizedDiscountedPrice, string Discount)
    {
        if(ItemsIn.Length > Counts.Length)
        {
            throw new ArgumentException($"Supplied counts for items dont match ({ItemsIn.Length} items vs {Counts.Length} counts)");
        }
        Lable.text = lable;
        Description.text = desc;

        ItemSO ResultItem = ItemsManager.Instance.GetItem(resultItem);
        if(ResultItem == null)
        {
            throw new NullReferenceException($"Item {resultItem} not found");
        }
        OfferPromoIcon.sprite = ResultItem.ItemPromoIcon;
        for (int i = 0; i < CurrentItems.Count; i++)
        {
            Destroy(gameObject);
        }
        CurrentItems.Clear();
        int mx = Mathf.Min(ItemsIn.Length, MaximumItemsInOffer);
        if (mx != ItemsIn.Length)
        {
            Debug.LogWarning($"Offer contains more items, then view supports ({ItemsIn.Length} in offer vs {MaximumItemsInOffer} maximum)");
        }
        for (int i = 0; i < mx; i++)
        {
            GameObject NGO = parentWindow.CreateItemIcon(ItemsIn[i], Counts[i]).gameObject;
            NGO.transform.SetParent(ItemsRoot);
        }


        BuyButton.SetData(ShopItemUID, LocalizedPrice, LocalizedDiscountedPrice, Discount);
    }
}
