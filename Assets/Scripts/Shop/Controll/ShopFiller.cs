using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShopFiller : MonoBehaviour, IShopFiller
{
    public UI_ShopWindow MyWindow;

    public void OpenWindow()
    {
        MyWindow.Clear();
        MyWindow.Open();
        FillShop(MyWindow);
    }

    public void FillShop(IShopWindowView W)
    {
        List<ShopItemSO> Items = ShopManager.Instance.GetAvaliableOffers();
        for(int i  = 0; i < Items.Count; i++)
        {
            ShopItemSO I = Items[i];
            IShopItemView View = W.CreateEmptyShopOfferView(I.ShopItemUID);
            string[] itms = new string[I.Items.Count];
            int[] counts = new int[I.Items.Count];
            for(int j = 0; j < I.Items.Count; j++)
            {
                itms[j] = I.Items[j].Item.ItemUID;
                counts[j] = I.Items[j].Count;
            }
            View.SetShopItem(I.ShopItemUID, I.OfferName, I.OfferDescription, I.ResultItem.ItemUID, itms, counts, I.GetPriceString(), I.GetDiscountedPriceString(), I.GetDiscountString());

            //View.SetShopItem(I);
        }
    }
}

public interface IShopWindowView
{
    public void AddAndFillShopOfferView(ShopItemSO I);
    public IShopItemView CreateEmptyShopOfferView(string UID);
    public void RemoveOfferView(string UID);
    public void Open();
    public void Close();
    public void Clear();

}