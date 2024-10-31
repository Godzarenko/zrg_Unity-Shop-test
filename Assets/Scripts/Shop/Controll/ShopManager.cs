using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    static ShopManager _instance;
    public static ShopManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<ShopManager>();
            }
            return _instance;
        }
    }

    [SerializeField] List<ShopItemSO> AllOffers;
    [SerializeField] List<ShopItemSO> AvaliableOffers;
    [SerializeField] List<ShopItemSO> LoadedOffers;

    public ShopItemSO GetOffer(string offerUID)
    {
        for(int i = 0; i < AllOffers.Count; i++)
        {
            if (AllOffers[i].ShopItemUID == offerUID)
            {
                return AllOffers[i];
            }
        }
        Debug.LogError($"Offer {offerUID} not found");
        return null;
    }
    public List<ShopItemSO> GetAvaliableOffers()
    {
        List<ShopItemSO> Res = new List<ShopItemSO>();
        for(int i = 0; i < AvaliableOffers.Count; i++)
        {
            //check avaliablility, time stamps, count bought
            Res.Add(AvaliableOffers[i]);
        }
        return Res;
    }
    public void TryBuyOffer(string UID)
    {
        ShopItemSO Offer = GetOffer(UID);
        if (Offer == null)
        {
            throw new NullReferenceException($"Offer {UID} not found");
        }
        Debug.Log($"Try buy offer {UID}");
    }
    public void RegisterLoadedOffer(ShopItemSO NSI)
    {
        LoadedOffers.Add(NSI);
        AllOffers.Add(NSI);
    }

    public ShopItemSO LoadNewOfferJSON(string json)
    {
        try
        {
            SerializedShopItemSO NSSI = JsonUtility.FromJson<SerializedShopItemSO>(json);
            ShopItemSO NSI = NSSI.SerializedItem;

            for (int i = 0; i < AllOffers.Count; i++)
            {
                if (AllOffers[i].ShopItemUID == NSI.ShopItemUID)
                {
                    //Offer exists
                    //Merge strategy?
                    return AllOffers[i];
                }
            }
            //offer not found, its new offer
            RegisterLoadedOffer(NSI);
            if (NSSI.IsAvaliable)
            {
                AvaliableOffers.Add(NSI);
            }
            return NSI;
        }
        catch (Exception e)
        {
            Debug.LogError("Cant parse Shop Offer from JSON", this);
            Debug.LogError("Got shop offer JSON:\n" + json, this);
            Debug.LogError(e, this);
            return null;
        }
    }
}


public interface IShopFiller
{
    public void FillShop(IShopWindowView W);
}