using UnityEngine;
using UnityEngine.UI;

public class Button_ShopSell:MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (GameController.instance.ShopController.IsMerchantHasPlace())
            {
                int price = GameManager.instance.PriceResource.GetPriceResource(GameController.instance.ShopController.SelectedResuorce.ResourseType);
                GameController.instance.Gold += (int)(price * ((100 - GameManager.PercentageOfMoneyLostOnSale) / 100f));
                GameController.instance.ShopController.UpdateGold();
                GameController.instance.ShopController.SelectedResuorce.Reposition();
                GameController.instance.ShopController.Sell();
            }
        });
    }
}

