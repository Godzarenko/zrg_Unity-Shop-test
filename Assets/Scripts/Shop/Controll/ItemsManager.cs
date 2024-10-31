using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    static ItemsManager _instance;
    public static ItemsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<ItemsManager>();
            }
            return _instance;
        }
    }

    public List<ItemSO> AllItems;

    public ItemSO GetItem(string itemUID)
    {
        for(int i = 0; i < AllItems.Count; i++)
        {
            if (AllItems[i].ItemUID == itemUID)
            {
                return AllItems[i];
            }
        }
        Debug.LogError($"Item {itemUID} not found");
        return null;
    }
}
