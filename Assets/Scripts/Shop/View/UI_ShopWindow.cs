using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ShopWindow : MonoBehaviour, IShopWindowView
{
    public UI_Item ItemPrefab;
    public UI_ShopItem ShopItemPrefab;

    public Transform OffersRoot;

    Dictionary<string, UI_ShopItem> CurrentOffers = new Dictionary<string, UI_ShopItem>();

    public UI_Item CreateItemIcon(ItemSO I, int Count)
    {
        if (I == null)
        {
            throw new NullReferenceException("Item cant be NULL");
        }
        if (Count <= 0)
        {
            throw new ArgumentOutOfRangeException("Item count must be > 0");
        }
        UI_Item NewItem = Instantiate(ItemPrefab);
        NewItem.SetItem(I, Count);
        return NewItem;
    }
    public UI_Item CreateItemIcon(string I, int Count)
    {
        if (I == null || I == "")
        {
            throw new NullReferenceException("Item cant be NULL");
        }
        if (Count <= 0)
        {
            throw new ArgumentOutOfRangeException("Item count must be > 0");
        }
        UI_Item NewItem = Instantiate(ItemPrefab);
        NewItem.SetItem(I, Count);
        return NewItem;
    }

    public void AddAndFillShopOfferView(ShopItemSO I)
    {
        UI_ShopItem NGO = Instantiate(ShopItemPrefab, OffersRoot);
        NGO.parentWindow = this;
        NGO.SetShopItem(I);
        CurrentOffers.Add(I.ShopItemUID, NGO);
    }
    public IShopItemView CreateEmptyShopOfferView(string UID)
    {
        UI_ShopItem NGO = Instantiate(ShopItemPrefab, OffersRoot);
        NGO.parentWindow = this;
        CurrentOffers.Add(UID, NGO);
        return NGO;
    }
    public void RemoveOfferView(string UID)
    {
        if (CurrentOffers.ContainsKey(UID))
        {
            Destroy(CurrentOffers[UID].gameObject);
            CurrentOffers.Remove(UID);
        }
    }
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    public void Clear()
    {
        foreach(var item in CurrentOffers)
        {
            Destroy(item.Value.gameObject);
        }
        CurrentOffers.Clear();
    }
}
