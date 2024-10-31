using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ShopBuyButton : MonoBehaviour
{
    [SerializeField] string ShopItemUID;

    [Header("No discount")]
    [SerializeField] GameObject NormalGroup;
    [SerializeField] TMPro.TextMeshProUGUI CostText;
    [Header("Discount")]
    [SerializeField] GameObject DiscountedGroup;
    [SerializeField] TMPro.TextMeshProUGUI dicount_CostText;
    [SerializeField] TMPro.TextMeshProUGUI dicount_DiscountedCostText;
    [SerializeField] TMPro.TextMeshProUGUI dicount_Discount;

    public void SetData(string shopItemUID, string Cost, string DiscountCost, string Discount)
    {
        ShopItemUID = shopItemUID;
        if(Cost != DiscountCost)
        {
            NormalGroup.SetActive(false);
            DiscountedGroup.SetActive(true);
            dicount_CostText.text = Cost;
            dicount_DiscountedCostText.text = DiscountCost;
            dicount_Discount.text = Discount;
        }
        else
        {
            NormalGroup.SetActive(true);
            DiscountedGroup.SetActive(false);
            CostText.text = Cost;
        }
    }

    public void OnClick()
    {
        ShopManager.Instance.TryBuyOffer(ShopItemUID);
    }
}
