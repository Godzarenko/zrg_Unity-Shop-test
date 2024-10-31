using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Item : MonoBehaviour, IItemView
{
    ItemSO CurrentItem;
    public UnityEngine.UI.Image ItemImage;
    public TMPro.TextMeshProUGUI ItemCount;

    public void SetItem(ItemSO I, int Count)
    {
        if (I == null)
        {
            throw new NullReferenceException("Item cant be NULL");
        }
        if(Count <= 0)
        {
            throw new ArgumentOutOfRangeException("Item count must be > 0");
        }
        if (CurrentItem != I)
        {
            CurrentItem = I;
            ItemImage.sprite = I.ItemIcon;
            ItemCount.text = Count.ToString();
        }
    }
    public void SetItem(Sprite Icon, int Count)
    {
        if (Icon == null)
        {
            throw new NullReferenceException("Item cant be NULL");
        }
        if (Count <= 0)
        {
            throw new ArgumentOutOfRangeException("Item count must be > 0");
        }
        CurrentItem = null; //no reference to model
        ItemImage.sprite = Icon;
        ItemCount.text = Count.ToString();
    }
    public void SetItem(string UID, int Count)
    {
        if (UID == null || UID == "")
        {
            throw new NullReferenceException("Item cant be NULL");
        }
        if (Count <= 0)
        {
            throw new ArgumentOutOfRangeException("Item count must be > 0");
        }
        ItemSO Item = ItemsManager.Instance.GetItem(UID);
        SetItem(Item, Count);
    }
}
