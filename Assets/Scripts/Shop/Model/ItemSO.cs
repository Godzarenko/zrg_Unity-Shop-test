using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ItemSO", menuName = "Shop/Item")]
public class ItemSO : ScriptableObject
{
    public string ItemUID;
    public Sprite ItemIcon;
    public Sprite ItemPromoIcon;
    public string ItemName;
}
public interface IItemView
{
    public void SetItem(Sprite Icon, int count);
    public void SetItem(ItemSO Item, int count);
    public void SetItem(string ItemUID, int count);
}